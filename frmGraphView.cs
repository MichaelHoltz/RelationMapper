
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelationMap.Models;
using System.Diagnostics;
using Microsoft.Msagl;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Core.Geometry.Curves;
using P2 = Microsoft.Msagl.Core.Geometry.Point;
using Microsoft.Msagl.Layout.MDS;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Drawing;
using DrawingEdge = Microsoft.Msagl.Drawing.Edge;
using DrawingNode = Microsoft.Msagl.Drawing.Node;
using DrawingColor = Microsoft.Msagl.Drawing.Color;
using Color = System.Drawing.Color;
using Microsoft.Msagl.Routing;


namespace RelationMap
{
    public partial class frmGraphView : Form
    {
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        
        //Graph graph;
        Graph master;
        readonly MdsLayoutSettings mdsLayoutSettings;
        readonly SugiyamaLayoutSettings sugiyamaSettings;
        LayerDirection userLayerDirection = LayerDirection.LR;
        PlaneTransformation planeTB = new PlaneTransformation(); // Fix bug where they don't expect to be rotating and this is the nothing default
        string selectedNode = "Virtual Servers";
        object selectedObject;
        AttributeBase selectedObjectAttr;

        Dictionary<DrawingEdge, AttributeBase> highlightedEdges = new Dictionary<DrawingEdge, AttributeBase>();
        
        

        List<DrawingEdge> aeMaster = new List<DrawingEdge>(); // All edges to prevent duplicates
        List<Image> imageList = new List<Image>(); // Image list to reduce file movement to just once
        Universe u = new Universe();

        DrawingColor MovieColor = DrawingColor.PaleGreen;
        DrawingColor StudioColor = DrawingColor.PowderBlue;
        DrawingColor FranchiseColor = DrawingColor.PaleGoldenrod;
        DrawingColor CharacterColor = DrawingColor.BurlyWood;
        //Microsoft.Msagl.Drawing.Color WebToDBColor = Microsoft.Msagl.Drawing.Color.Green;
        DrawingColor ActorColor = DrawingColor.PaleVioletRed;
        DrawingColor GatewayColor = DrawingColor.YellowGreen;

        public frmGraphView()
        {
            //---------------------------------------------------------------------------------------
            //Remove Debug Asserts from Graph Tool - may have adverse side effects elsewhere though!!
            Debug.Listeners.Clear();
            //---------------------------------------------------------------------------------------
            mdsLayoutSettings = new MdsLayoutSettings();
            sugiyamaSettings = new SugiyamaLayoutSettings();
            InitializeComponent();
            SuspendLayout();
            
            viewer.Dock = DockStyle.Fill;
            //Mouse Double Click Handler
            viewer.MouseDoubleClick += Viewer_MouseDoubleClick;
            //Mouse over / Object select Handler
            viewer.ObjectUnderMouseCursorChanged += Viewer_ObjectUnderMouseCursorChanged;
            //Key Press handler
            viewer.KeyPress += Viewer_KeyPress;
            viewer.KeyDown += Viewer_KeyDown;
            viewer.KeyUp += Viewer_KeyUp;
            //Add viewer
            Controls.Add(viewer);
            //Make it dock under the top panel
            viewer.BringToFront();
            viewer.OutsideAreaBrush = new SolidBrush(Color.White);
            ResumeLayout();
        }

        private void Viewer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString().Contains("Control"))
            {
                viewer.PanButtonPressed = false;
            }
        }

        private void Viewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString().Contains("Control"))
            {
                viewer.PanButtonPressed = true;
            }
        }

        private void frmGraphView_Load(object sender, EventArgs e)
        {
            LoadImages(PrivateData.GetRelativePath(@"\Cache\Images\")); // Load all known images for use in the Graph
            u = PersistanceBase.Load<Universe>(PrivateData.GetRelativePath(@"\Cache\uinverse2.json"));
            refreshLists();
            //viewer.Graph = SetViewAll();
            lblSelectedNode.Text = "Selected Node: " + selectedNode;
        }
        private void refreshLists()
        {
            //Clear StudioGroup List 
            cbStudios.Items.Clear();
            cbStudios.Items.Add("All");
            //Load All StudioGroups
            cbStudios.Items.AddRange(u.StudioGroups.Select(o => o.Name).ToArray());
            cbStudios.SelectedIndex = 0;
        }
        private Graph getNewGraph(string name)
        {
            Graph graph = new Graph(name);
            graph.LayoutAlgorithmSettings = master.LayoutAlgorithmSettings;
            graph.Attr.LayerSeparation = master.Attr.LayerSeparation;
            graph.Attr.LayerDirection = master.Attr.LayerDirection;// = userLayerDirection; // LayerDirection.None; //.LR;
            return graph;
        }
        /// <summary>
        /// Returns the tool tip based on the userData of a node
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        private String getTip(object userData)
        {
            String tip = "";
            //Old Code from original use - Saved for example.
            //if (userData is Server)
            //{

            //    Server o = (userData as Server);
            //    toolTip1.ToolTipTitle = "FWDM " + o.type + " Server";
            //    ODD Spacing is here to make things line up(as well as possible, with non -fixed width fonts)
            //        tip = "Computer Name:   " + o.computer_name + "\r\n" +
            //              "Ip Address:              " + o.ip_address + "\r\n" +
            //              "Description:            " + o.description + "\r\n" +
            //              "OS:                           " + o.os + "\r\n" +
            //              "System                    " + o.system + "\r\n" +
            //              "Template:               " + o.vm_template + "\r\n" +
            //              "State:                       " + o.state;
            //}

            return tip;
        }
        /// <summary>
        /// Allows Object Identification by moving the mouse over a node or edge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_ObjectUnderMouseCursorChanged(object sender, ObjectUnderMouseCursorChangedEventArgs e)
        {
            //////viewer.ObjectUnderMouseCursor vs viewer.SelectedObject??
            if (viewer.SelectedObject != null && viewer.SelectedObject is DrawingNode)
            {
                viewer.Focus();
                //    // toolTip1.SetToolTip(viewer, (viewer.SelectedObject as Microsoft.Msagl.Drawing.Node).Id);
                //    viewer.SetToolTip(toolTip1, getTip(e.NewObject.DrawingObject.UserData));
                selectedNode = (viewer.SelectedObject as DrawingNode).Id;
                //    lblSelectedNode.Text = "Selected Node: " + selectedNode;
            }
            //else
            //{
            //    viewer.SetToolTip(toolTip1, "");
            //}
            selectedObject = e.OldObject != null ? e.OldObject.DrawingObject : null;
            //Clear Selected Object
            if (selectedObject != null && Form.ModifierKeys != Keys.Control)
            {
                RestoreSelectedObjAttr();
                viewer.Invalidate(e.OldObject);
                selectedObject = null;
            }

            if (viewer.ObjectUnderMouseCursor == null)
            {
                lblSelectedNode.Text = "Selected Object: ";
                viewer.SetToolTip(toolTip1, "");
                infoTip1.ClearData();
                RemoveHighlightedPath();
            }
            else {
                selectedObject = viewer.ObjectUnderMouseCursor.DrawingObject;


                if (selectedObject is DrawingEdge && Form.ModifierKeys != Keys.Control)
                {
                    DrawingEdge edge = selectedObject as DrawingEdge;
                    selectedObjectAttr = edge.Attr.Clone();
                    //edge.Attr.Color = DrawingColor.Blue;
                    edge.Attr.LineWidth += 3;
                    viewer.Invalidate(e.NewObject);
                    lblSelectedNode.Text = "Selected Edge: " + selectedObject.ToString();
                    //         here we can use e.Attr.Id or e.UserData to get back to the user data
                    // viewer.SetToolTip(toolTip1, String.Format("edge from {0} to {1}", edge.Source, edge.Target));
                }
                if (selectedObject is DrawingNode && Form.ModifierKeys != Keys.Control)
                {
                    DrawingNode node = selectedObject as DrawingNode;
                    selectedObjectAttr = node.Attr.Clone();

                    //node.Attr.Color = DrawingColor.Blue;
                    node.Attr.LineWidth += 4;
                    // //   here you can use e.Attr.Id to get back to your data
                    infoTip1.SetData(e.NewObject.DrawingObject.UserData);
                    //viewer.SetToolTip(toolTip1, getTip(e.NewObject.DrawingObject.UserData));

                    selectedNode = (viewer.SelectedObject as DrawingNode).Id;
                    lblSelectedNode.Text = "Selected Node: " + selectedNode;
                    //viewer.SetToolTip(toolTip1,
                    //                   String.Format("node {0}",
                    //                                 (selectedObject as Microsoft.Msagl.Drawing.Node).Attr.Id));
                    viewer.Invalidate(e.NewObject);


                    RemoveHighlightedPath(); //Remove last path - Incase traveled by edge to this node.
                    HighlightFullPath(node, true, true); // Recursive
                    foreach (IViewerObject ee in viewer.Entities.Where(ee => ee is IViewerEdge))
                        viewer.Invalidate(ee);
                }

            }
        }
        void RestoreSelectedObjAttr()
        {

            if (selectedObject is DrawingEdge && selectedObjectAttr != null && selectedObjectAttr is EdgeAttr)
            {
                DrawingEdge edge = selectedObject as DrawingEdge;
                edge.Attr = ((EdgeAttr)selectedObjectAttr).Clone();

            }
            if (selectedObject is DrawingNode && selectedObjectAttr != null && selectedObjectAttr is NodeAttr)
            {
                DrawingNode node = selectedObject as DrawingNode;
                node.Attr = ((NodeAttr)selectedObjectAttr).Clone();
                node.Attr.LineWidth = 1; // Fix bug

            }
            selectedObjectAttr = null;
            selectedObject = null;

        }
        private void RemoveHighlightedPath()
        {
            foreach (DrawingEdge item in highlightedEdges.Keys)
            {
                item.Attr = ((EdgeAttr)highlightedEdges[item]).Clone();
            }
            foreach (IViewerObject ee in viewer.Entities.Where(ee => ee is IViewerEdge))
                viewer.Invalidate(ee);
            highlightedEdges = new Dictionary<DrawingEdge, AttributeBase>(); // Reset Edges
        }
        private void HighlightFullPath(DrawingNode node, bool backward, bool forward)
        {
            if (node.Id == "Manual_Build" && forward)
            {

            }
            if (node.Id == "Templates" && forward)
            {

            }
            if (forward)
            {
                if (node.OutEdges.Count() == 0)
                {
                    HighlightFullPath(node, true, false); // At the end and want to work back.
                }
                foreach (DrawingEdge item in node.OutEdges)
                {
                    try { 
                    highlightedEdges.Add(item, item.Attr.Clone()); // Save original State.
                    item.Attr.LineWidth = 3;
                    item.Attr.Color = DrawingColor.Red;
                    HighlightFullPath(item.TargetNode, false, true); // Get next nodes
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine(e.Message);
                    }
                }
            }
            if (backward)
            {
                foreach (DrawingEdge item in node.InEdges)
                {
                    try
                    {
                        highlightedEdges.Add(item, item.Attr.Clone()); // Save original State.
                        item.Attr.LineWidth = 3;
                        item.Attr.Color = DrawingColor.Red;
                        //Rectangle r = new Rectangle((int)item.BoundingBox.Left, (int)item.BoundingBox.Top, (int)item.BoundingBox.Width, (int)item.BoundingBox.Height);
                        //viewer.Invalidate(r);

                        HighlightFullPath(item.SourceNode, true, false); // Get next nodes
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine(e.Message);
                    }
                }
            }

        }
        /// <summary>
        /// Remove all out items from a given node
        /// </summary>
        /// <param name="n"></param>
        private void RemoveAllOutItems(DrawingNode n)
        {
            var noe = n.OutEdges.ToArray(); //out edges to array
            for (int i = 0; i < noe.Length; i++)
            {
                int inCount = noe[i].TargetNode.InEdges.Count();
                int outCount = noe[i].TargetNode.OutEdges.Count();
                master.RemoveEdge(noe[i]);
                aeMaster.Remove(aeMaster.Find(o => o.Source == noe[i].Source && o.Target == noe[i].Target));
                if (inCount == 1)
                {
                    noe[i].DrawEdgeDelegate = null;
                    if (outCount > 0)
                    {
                        RemoveAllOutItems(noe[i].TargetNode);//Remove all children
                    }
                    master.RemoveNode(noe[i].TargetNode);
                }
            }
        }
        private void ToggleCharactersForMovie(DrawingNode n)
        {
            // Already have Characters showing // remove all Out Edges and descendants that only come from here.
            if (n.OutEdges.Count() > 0) 
            {
                RemoveAllOutItems(n);
            }
            else
            {
                Movie m = n.UserData as Movie;
                String movieID = m.Title + " (" + m.ReleaseYear + ")";
                foreach (Character c in m.Characters)
                {
                    AE(master, aeMaster, movieID, c.Name, CharacterColor, c);
                    SetNodeDelegate(FN(master, c.Name)); // Allows this node to be custom draw

                    //    foreach (Actor a in m.GetActorsWhoPlayedCharacter(c.Name))
                    //    {
                    //        AE(master, aeMaster, c.Name, a.Name, ActorColor, a);
                    //        SetNodeDelegate(FN(master, a.Name)); // Allows this node to be custom draw

                    //    }

                }
            }
        }
        private void ToggleActorsForCharacter(DrawingNode n)
        {
            // Already have Characters showing // remove all Out Edges and descendants that only come from here.
            if (n.OutEdges.Count() > 0)
            {
                RemoveAllOutItems(n);
            }
            else
            {
                Character c = n.UserData as Character;
                String movieID = c.Name; // + " (" + m.ReleaseYear + ")";
                //////Compile Error now that Character has Actor ID.
                ////foreach (Person a in c.Actors)
                ////{
                ////    AE(master, aeMaster, movieID, a.Name, ActorColor, a);
                ////    SetNodeDelegate(FN(master, a.Name)); // Allows this node to be custom draw

                ////    //    foreach (Actor a in m.GetActorsWhoPlayedCharacter(c.Name))
                ////    //    {
                ////    //        AE(master, aeMaster, c.Name, a.Name, ActorColor, a);
                ////    //        SetNodeDelegate(FN(master, a.Name)); // Allows this node to be custom draw

                ////    //    }

                ////}
            }
        }
        /// <summary>
        /// Handler to allow different perspectives to be Drawn
        /// 
        /// selectedNode is set when the mouse is over a node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_KeyPress(object sender, KeyPressEventArgs e)
        {
            DrawingNode n = FN(master, selectedNode);
            switch (e.KeyChar.ToString().ToUpper())
            {
                case "C":
                    if (n.UserData is Movie)
                    {
                        ToggleCharactersForMovie(n);
                    }
                    break;
                case "A":
                    if (n.UserData is Character)
                    {
                        ToggleActorsForCharacter(n);
                    }
                    if (n.UserData is Movie)
                    {
                        //Might not have characters showing so would need to expand Characters first.
                        if (n.OutEdges.Count() == 0)
                        {
                            ToggleCharactersForMovie(n);
                        }
                        foreach (DrawingEdge de in n.OutEdges  )
                        {
                            ToggleActorsForCharacter(de.TargetNode);
                        }
                    }
                    break;
                default:
                    break;
            }
            ////Update viewer graph
            viewer.NeedToCalculateLayout = true;
            viewer.Graph = master;
            viewer.NeedToCalculateLayout = false;

        }
        /// <summary>
        /// Function to Generate a "Level1" Graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private Graph GenerateOneLevelFromNode(Graph graph, List<DrawingEdge> edgeList, DrawingNode n, Boolean inEdges, Boolean outEdges)
        {
            // n is the selected node
            //Console.WriteLine("GenerateOneLevelFromNode: " + n);
            //Enable in Edges
            if (inEdges)
            {
                foreach (DrawingEdge item in n.InEdges)
                {

                    //if (AE(graph, edgeList, item.SourceNode.Id, n.Id, item.SourceNode.Attr.FillColor, item.SourceNode.UserData))
                    if (AE(graph, edgeList, item.SourceNode.Id, n.Id, n.Attr.FillColor, n.UserData))
                    {
                        // Console.WriteLine("InEdge: " + item);

                        //Source Node
                        DrawingNode mx = FN(master, item.SourceNode.Id); // Lookup Master for Attribute
                        DrawingNode x = graph.FindNode(item.SourceNode.Id);
                        x.Attr = mx.Attr.Clone();
                        x.Attr.LineWidth = 1; // Fix selection Bug
                        x.UserData = mx.UserData;


                        //Set selected node attributes
                        mx = FN(master, n.Id); // Lookup Master for Attribute
                        x = graph.FindNode(n.Id);
                        x.Attr = mx.Attr.Clone();
                        x.Attr.LineWidth = 1; // Fix selection Bug
                        x.UserData = mx.UserData;
                        
                        //Recursively add in Edges and nodes
                        graph = GenerateOneLevelFromNode(graph, edgeList, FN(master, item.SourceNode.Id), true, false);

                    }
                }
            }
            //Enable out Edges
            if (outEdges)
            {
                foreach (DrawingEdge item in n.OutEdges)
                {
                    
                    // Use the Node
                    if (AE(graph, edgeList, n.Id, item.TargetNode.Id, n.Attr.FillColor, item.SourceNode.UserData))
                    {

                        // Console.WriteLine("OutEdge: " + item);
                        //Target Node
                        DrawingNode mx = FN(master, item.TargetNode.Id); // Lookup Master for Attribute
                        DrawingNode x = FN(graph, item.TargetNode.Id); //Lookup Target Node
                        x.Attr = mx.Attr.Clone();
                        x.Attr.LineWidth = 1; // Fix selection Bug
                        x.UserData = mx.UserData;

                        //Source Node
                        mx = FN(master, n.Id); // Lookup Master for Attribute
                        x = FN(graph,n.Id);     //Lookup graph Node
                        x.Attr = mx.Attr.Clone();
                        x.Attr.LineWidth = 1; // Fix selection Bug
                        x.UserData = mx.UserData;
                        //Recursively add out Edges and nodes
                        graph = GenerateOneLevelFromNode(graph, edgeList, FN(master, item.TargetNode.Id), false, true);
                    }
                }
            }
            return graph;
        }

        /// <summary>
        /// Double Clicking specific nodes will reset the graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if(e.Button == System.Windows.Forms.MouseButtons.Left)
                //if (viewer.SelectedObject != null && viewer.SelectedObject is DrawingNode)
                //{
                //    DrawingNode node = viewer.SelectedObject as DrawingNode;
                //    if (node.Id == "Virtual Machines" || node.Id == "WEB Servers" || node.Id == "DB Servers")
                //    {
                //        viewer.NeedToCalculateLayout = true;
                //        viewer.Graph = SetViewAll();
                //        viewer.NeedToCalculateLayout = false;
                //    }
                //}
        }


        private void cbStudios_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cbStudios.SelectedIndex >= 0)
            //{
            //    //lbMovies.Items.Clear();
            //    //lbTvShows.Items.Clear();

            //    cbFranchises.Items.Clear();
            //    cbFranchises.Items.Add("All");
            //    cbFranchises.Items.Add("None");
            //    String studioStr = cbStudios.SelectedItem.ToString();
            //    if (studioStr == "All")
            //    {
            //        cbFranchises.Items.AddRange(u.GetAllFranchises().Select(o => o.Name).ToArray());
            //    }
            //    else {
            //        StudioGroup s = u.GetStudio(studioStr);
            //        cbFranchises.Items.AddRange(u.GetAllFranchises(s).Select(o => o.Name).ToArray());
            //    }

            //    cbFranchises.SelectedIndex = 0;
            //    //HandleAddingButtons();
            //}
        }

        private void cbFranchises_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbFranchises.SelectedIndex >= 0)
            {
                String studioStr = cbStudios.SelectedItem.ToString();
                String franchiseStr = cbFranchises.SelectedItem.ToString();

                //if (studioStr == "All") // All franchises
                //{
                //    foreach (StudioGroup s in u.StudioGroups)
                //    {
                //    }
                //}
                //else // Sepecific Franchise Choosen.
                //{
                //}
                viewer.NeedToCalculateLayout = true;
                viewer.Graph = SetViewAll();
                viewer.NeedToCalculateLayout = false;
            }
        }

        /// <summary>
        /// Master Graph with Everything available based on check 
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        private Graph SetViewAll(int maxLevel =10)
        {
            //create the graph content 
            //create a graph object 
            master = new Graph("master");
            aeMaster = new List<DrawingEdge>(); // All edges to prevent duplicates

            master.LayoutAlgorithmSettings = mdsLayoutSettings;
            //master.LayoutAlgorithmSettings = sugiyamaSettings;
            master.Attr.LayerSeparation = 50;
            master.Attr.LayerDirection = userLayerDirection; // LayerDirection.None; //.LR;


            List<DrawingNode> nodeListTop = new List<DrawingNode>(); // List for Constraint
            //List<DrawingNode> nodeListTemplateMaster = new List<DrawingNode>(); // List for Constraint
            //List<DrawingNode> nodeListTemplates = new List<DrawingNode>(); // List for Constraint
            List<DrawingNode> nodeListStudios = new List<DrawingNode>(); // List for Constraint
            List<DrawingNode> nodeListDBServers = new List<DrawingNode>(); // List for Constraint

            //List<DrawingNode> nodeListGateways = new List<DrawingNode>(); // List for Constraint
            //List<DrawingNode> nodeListLoaders = new List<DrawingNode>(); // List for Constraint
            master.LayerConstraints.RemoveAllConstraints();

            HashSet<StudioGroup> StudioGroups = u.StudioGroups;
            //HashSet<Franchise> franchises = u.GetAllFranchises();
            HashSet<Franchise> franchises = null;
            HashSet<Movie> movies = u.GetAllMovies();

            String studioStr = cbStudios.SelectedItem.ToString();
            String franchiseStr = cbFranchises.SelectedItem.ToString();
            if (studioStr != "All")
            {
                StudioGroups = new HashSet<StudioGroup>();
                StudioGroups.Add(u.GetStudio(studioStr));
            }
            //if (franchiseStr != "All" && franchiseStr != "None")
            //{
            //    franchises = new HashSet<Franchise>();
            //    franchises.Add(StudioGroups.First().GetFranchise(franchiseStr));
            //}
            //StudioGroups
            foreach (StudioGroup s in StudioGroups)
            {
                //AE(master, aeMaster, "WEB Servers", item.Name, WebColor, item);
                master.AddNode(s.Name).Attr.FillColor = StudioColor;
                FN(master, s.Name).UserData = s;
                nodeListStudios.Add(FN(master, s.Name));
                SetNodeDelegate(FN(master, s.Name)); // Allows this node to be custom drawn
                ////Franchises
                //foreach (Franchise f in s.Franchises)
                //{
                //    AE(master, aeMaster, s.Name, f.Name, FranchiseColor, f);
                //    SetNodeDelegate(FN(master, f.Name)); // Allows this node to be custom draw

                //    foreach (Movie m in u.GetAllMovies(f))
                //    {
                //        String movieID = m.Title + " (" + m.ReleaseYear + ")";
                //        AE(master, aeMaster, f.Name, movieID, MovieColor, m);
                //        SetNodeDelegate(FN(master, movieID)); // Allows this node to be custom draw
                //        //foreach (Character c in m.Characters)
                //        //{
                //        //    AE(master, aeMaster, movieID, c.Name, CharacterColor, c);
                //        //    SetNodeDelegate(FN(master, c.Name)); // Allows this node to be custom draw

                //        //    foreach (Actor a in m.GetActorsWhoPlayedCharacter(c.Name))
                //        //    {
                //        //        AE(master, aeMaster, c.Name, a.Name, ActorColor, a);
                //        //        SetNodeDelegate(FN(master, a.Name)); // Allows this node to be custom draw

                //        //    }

                //        //}

                //    }
                //}
                //Movies Not in a Franchise
                foreach (Movie m in u.GetAllMoviesNotInAnyFranchise(s))
                {
                    String movieID = m.Title + " (" + m.ReleaseYear + ")";
                    AE(master, aeMaster, s.Name, movieID, MovieColor, m);
                    SetNodeDelegate(FN(master, movieID)); // Allows this node to be custom draw
                }

            }
            return master;
           
        }

        /// <summary>
        /// Function to Find a Node in the Graph
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private DrawingNode FN(Graph g, String ID)
        {
            return g.FindNode(ID);
        }
        
        /// <summary>
        /// Function to Add and Edge to the Graph (Only if it doesn't already Exist)
        /// Assigns to user Data and Attributes to Target
        /// </summary>
        /// <param name="sourceID"></param>
        /// <param name="targetID"></param>
        /// <param name="nodeColor"></param>
        /// <param name="userData"></param>
        private Boolean AE(Graph g, List<DrawingEdge> edgeList, String sourceID, String targetID, DrawingColor nodeColor, object userData)
        {   //Search for Edge in allEdgesMaster before adding it.
            bool isUnique = true; // Add by default
            foreach (DrawingEdge item in edgeList)
            {
                if (item.Source == sourceID && item.Target == targetID)
                {
                    isUnique = false;
                    break;
                }
            }
            if (isUnique)
            {
                //Create an edge
                DrawingEdge edge = new DrawingEdge(sourceID, null, targetID);
                edgeList.Add(edge); // Add edge to list for keeping Unique Edges

                g.AddEdge(sourceID, targetID).Attr.Color = nodeColor; //Add Edge

                //FN(g, sourceID).Attr.FillColor = nodeColor;
                //FN(g, sourceID).UserData = userData;


                FN(g, targetID).Attr.FillColor = nodeColor;
                FN(g, targetID).UserData = userData;



            }
            return isUnique;
        }
        private void rbLR_CheckedChanged(object sender, EventArgs e)
        {

            if (rbLR.Checked)
                userLayerDirection = LayerDirection.LR;
            if (rbTB.Checked)
            {
                userLayerDirection = LayerDirection.TB;
                sugiyamaSettings.Transformation = planeTB;
            }
            if (rbRL.Checked)
                userLayerDirection = LayerDirection.RL;
            if (rbBT.Checked)
                userLayerDirection = LayerDirection.BT;
            regenerateMaster();
        }
        private void regenerateMaster()
        {
            master = SetViewAll();
            //viewer.Graph = g;
            #region deal with serious bug causing null references when updating the graph
            //-------------------------------------------------------------------------
            viewer.Graph.LayerConstraints.RemoveAllConstraints();
            viewer.Graph.GeometryGraph = null;
            viewer.Graph = null;
            
            System.Threading.Thread.Sleep(100);
            //--------------------------------------------------------------------------
            #endregion
            viewer.NeedToCalculateLayout = true;
            viewer.Graph = master;
            //foreach (IViewerObject ee in viewer.Entities.Where(ee => ee is IViewerEdge))
            //    viewer.Invalidate(ee);
            viewer.NeedToCalculateLayout = false;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            viewer.NeedToCalculateLayout = true;
            master.LayoutAlgorithmSettings = mdsLayoutSettings;
            viewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.MDS;
            //viewer.Invalidate();
            
            foreach (DrawingNode item in master.Nodes)
            {
                if(item.Attr.LineWidth > 1)
                    item.Attr.LineWidth = 1;
            }
            viewer.Graph = master;
            viewer.NeedToCalculateLayout = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewer.NeedToCalculateLayout = true;
            master.LayoutAlgorithmSettings = sugiyamaSettings;
            viewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.SugiyamaScheme;
            foreach (DrawingNode item in master.Nodes)
            {
                if (item.Attr.LineWidth > 1)
                    item.Attr.LineWidth = 1;
            }
            viewer.Graph = master;
            viewer.NeedToCalculateLayout = false;

        }
        private void ViewOptions_CheckedChanged(object sender, EventArgs e)
        {
            regenerateMaster();
        }


        #region Application Specific Code to handle nodes in non-default ways
        private void LoadImages(string path)
        {
            Image img = Image.FromFile(path + @"movie-icon-1.png");
            img.Tag = "Default"; //Tag image for later retrieval
            imageList.Add(img);
            img = Image.FromFile(path + "Marvel.jpg");
            img.Tag = "Marvel";
            imageList.Add(img);
            img = Image.FromFile(path + "DC.jpg");
            img.Tag = "DC";
            imageList.Add(img);
            img = Image.FromFile(path + "character-icon.png");
            img.Tag = "Character";
            imageList.Add(img);
            //for (int i = 0; i < 14; i++)

            //{
            //    img = Image.FromFile(path + "group" + i.ToString() + ".png");
            //    img.Tag = i.ToString(); // Tag image for later retrieval
            //    imageList.Add(img);
            //}

        }
        private Image getGroupImage(object userData)
        {
            //Need to figure out what type userData is and then get the groupID if it exists..
            string groupId = "Default";
            if (userData != null)
            {
                if (userData is StudioGroup)
                {
                    groupId = (userData as StudioGroup).Name.ToString();
                    Image j = imageList.Find(o => o.Tag.ToString() == groupId);
                    if (j == null)
                    {
                        j = imageList.Find(o => o.Tag.ToString() == "Default");
                    }
                    return ScaleImage(j, 160, 60);
                }
                if (userData is Character)
                {
                    groupId = "Character"; //(userData as Character).group.ToString();
                }
                //if (userData is Gateway)
                //{
                //    groupId = (userData as Gateway).group.ToString();
                //}
                //if (userData is Template)
                //{
                //    groupId = "Default";
                //}

            }
            Image i = imageList.Find(o => o.Tag.ToString() == groupId);
            if (i == null)
            {
                i = imageList.Find(o => o.Tag.ToString() == "Default");
            }
            return ScaleImage(i, 80, 30);

        }

        #region Universal Code to handle nodes in non-default ways.
        /// <summary>
        /// Generic Function to Scale an image
        /// 
        /// TODO - Not sure it does what I'm looking for as I don't want to loose quality by down scaling and it seems to do that.
        /// Just looking for dimensions to reduce while maintaining aspect ratio.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        public void SetNodeDelegate(DrawingNode dn)
        {
            if (dn != null)
            {
                dn.DrawNodeDelegate = new DelegateToOverrideNodeRendering(DrawNode); //Testing getting an image on the Graph
                dn.NodeBoundaryDelegate = new DelegateToSetNodeBoundary(GetNodeBoundary);
            }
        }

        #region Overloaded Code to make MSAGL work as desired
        /// <summary>
        /// Main Overloaded Node Drawing
        /// Functionality Copied from examples and by looking at how it is implemeted by Default
        /// 
        /// The major difference between this DrawNode and Default behavior is to allow an Image to be in the node
        /// For a visual Cue.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="graphics"></param>
        /// <returns></returns>
        bool DrawNode(DrawingNode node, object graphics)
        {
            Graphics g = (Graphics)graphics;
            Image image = getGroupImage(node.UserData);
            //Set Alpha on Given Fill Color so Image Can show Through..
            //node.Attr.FillColor = new DrawingColor(100, node.Attr.FillColor.R,node.Attr.FillColor.G,node.Attr.FillColor.B);
            Color border = Color.FromArgb(node.Attr.Color.R, node.Attr.Color.G, node.Attr.Color.B);
            Pen borderPen = new Pen(border, (float)node.Attr.LineWidth);
            SolidBrush borderBrush = new SolidBrush(border);

            Color fill = Color.FromArgb(200, node.Attr.FillColor.R, node.Attr.FillColor.G, node.Attr.FillColor.B);


            using (System.Drawing.Drawing2D.Matrix m = g.Transform)
            {
                using (System.Drawing.Drawing2D.Matrix saveM = m.Clone())
                {
                    System.Drawing.Drawing2D.GraphicsPath path = FillTheGraphicsPath(node.GeometryNode.BoundaryCurve);

                    g.SetClip(FillTheGraphicsPath(node.GeometryNode.BoundaryCurve));
                    using (var m2 = new System.Drawing.Drawing2D.Matrix(1, 0, 0, -1, 0, 2 * (float)node.GeometryNode.Center.Y))
                        m.Multiply(m2);

                    g.Transform = m;

                    g.FillPath(new SolidBrush(fill), path); //Fill the inside
                    g.DrawImage(image, new PointF((float)(node.GeometryNode.Center.X - node.GeometryNode.Width / 2) +2, (float)(node.GeometryNode.Center.Y - node.GeometryNode.Height / 2) + 2));
                    Font f = new Font(node.Label.FontName, (float)node.Label.FontSize, (System.Drawing.FontStyle)(int)node.Label.FontStyle);
                    g.DrawString(node.LabelText, f, new SolidBrush(ForeColor),
                        new PointF((float)(node.GeometryNode.Center.X - node.GeometryNode.Width / 2 + image.Width), (float)(node.GeometryNode.Center.Y - g.MeasureString(node.LabelText, f).Height / 2)));
                    g.DrawPath(borderPen, path); // Draw the Border

                    g.Transform = saveM;
                    g.ResetClip();
                }
            }
            image.Dispose();
            return true;//returning false would enable the default rendering
        }
        //Conversion helper
        static internal PointF PointF(P2 p) { return new PointF((float)p.X, (float)p.Y); }

        /// <summary>
        /// Code Copied from GraphEditor.cs in automatic-graph-layout\graphlayout\samples\editing\grapheditor.cs
        /// Modified to allow an image to be considered on the left side of the Node and still have space for the Label Text
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        ICurve GetNodeBoundary(Microsoft.Msagl.Drawing.Node node)
        {
            int ImagePadding = 4; 
            Graphics g = this.CreateGraphics();
            Image image = getGroupImage(node.UserData); // Just a sample Image
            //Image image = ImageOfNode(node);
            Font f = new Font(node.Label.FontName, (float)node.Label.FontSize, (System.Drawing.FontStyle)(int)node.Label.FontStyle);
            double width = image.Width + g.MeasureString(node.LabelText, f).Width + ImagePadding;
            double height = image.Height + ImagePadding;// + 30;
            return CurveFactory.CreateRectangleWithRoundedCorners(width, height, width * .02, width * .02, new P2());
            //return CurveFactory.CreateRectangleWithRoundedCorners( width, height, width * radiusRatio, height * radiusRatio, new P2());
        }
        /// <summary>
        /// Only works for Rounded Rectangles 
        /// </summary>
        /// <param name="iCurve"></param>
        /// <returns></returns>
        static System.Drawing.Drawing2D.GraphicsPath FillTheGraphicsPath(ICurve iCurve)
        {
            //if (iCurve is Polyline)
            //{
            //    var curve = ((Polyline)iCurve).PolylinePoints;
            //    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            //    foreach (var seg in curve)
            //        AddSegmentToPath(seg, ref path);
            //    return path;

            //}
            //else
            //{
            var curve = ((RoundedRect)iCurve).Curve;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            foreach (ICurve seg in curve.Segments)
                AddSegmentToPath(seg, ref path);
            return path;

            //}
        }

        private static void AddSegmentToPath(ICurve seg, ref System.Drawing.Drawing2D.GraphicsPath p)
        {
            const float radiansToDegrees = (float)(180.0 / Math.PI);
            LineSegment line = seg as LineSegment;
            if (line != null)
                p.AddLine(PointF(line.Start), PointF(line.End));
            else {
                CubicBezierSegment cb = seg as CubicBezierSegment;
                if (cb != null)
                    p.AddBezier(PointF(cb.B(0)), PointF(cb.B(1)), PointF(cb.B(2)), PointF(cb.B(3)));
                else {
                    Ellipse ellipse = seg as Ellipse;
                    if (ellipse != null)
                        p.AddArc((float)(ellipse.Center.X - ellipse.AxisA.Length), (float)(ellipse.Center.Y - ellipse.AxisB.Length),
                            (float)(2 * ellipse.AxisA.Length), (float)(2 * ellipse.AxisB.Length), (float)(ellipse.ParStart * radiansToDegrees),
                            (float)((ellipse.ParEnd - ellipse.ParStart) * radiansToDegrees));

                }
            }
        }

        #endregion Overloaded Code to make MSAGL work as desired

        #endregion Universal Code to handle nodes in non-default ways.

        #endregion Application Specific Code to handle nodes in non-default ways

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HashSet<DrawingNode> closeNodes = new HashSet<DrawingNode>();
            foreach (DrawingNode node in viewer.Graph.Nodes)
            {
                if (node.LabelText.ToUpper().Contains(tbSearch.Text.ToUpper()))
                {
                    //P2 nPoint = node.Pos;
                    P2 nCenterPoint = viewer.Graph.FindGeometryNode(node.Id).Center;
                    //viewer.MapSourceRectangleToScreenRectangle

                    closeNodes.Add(node); // Found Node
                    DrawingNode tNode = node;
                    //while (tNode != null)
                    //{
                    //    if (tNode.OutEdges.Count() > 0)
                    //    {
                    //        foreach (DrawingEdge item in tNode.OutEdges)
                    //        {
                    //            closeNodes.Add(item.TargetNode);
                    //            tNode = item.TargetNode; // Could stack or queue..
                    //        }
                    //    }
                    //    else
                    //    {
                    //        tNode = null;
                    //    }
                    //}
                    //tNode = node;
                    //while (tNode != null)
                    //{
                        if (tNode.InEdges.Count() > 0)
                        {
                            foreach (DrawingEdge item in tNode.InEdges)
                            {
                                closeNodes.Add(item.SourceNode);
                                tNode = item.SourceNode; // Could stack or queue..
                            }
                        }
                        else
                        {
                            tNode = null;
                        }
                    //}

                    //Point np = 
                    viewer.ShowGroup(closeNodes.ToArray());
                    //viewer.CenterToGroup(closeNodes.ToArray());
                    //viewer.CenterToPoint(nCenterPoint);
                    //viewer.Scree
                    //viewer.PerformAutoScale();
                }
            }
            viewer.Focus(); // Set Focus back to viewer
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBFO_revisited
{
    public partial class Form1 : Form
    {
        string characters;

        int nbState;
        Microsoft.Msagl.Drawing.Graph graph;
        List<Dictionary<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>>> graphEdges;
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        List<bool> accState;

        int nbState2;
        Microsoft.Msagl.Drawing.Graph graph2;
        List<Dictionary<char, KeyValuePair<int, Microsoft.Msagl.Drawing.Edge>>> graphEdges2;
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer2;
        List<bool> accState2;
        public Form1()
        {
            InitializeComponent();
            characters = "e01";
            for (int i = 0; i < characters.Length; i++)
            {
                comboBox6.Items.Add(characters[i]);
            }

            nbState = 0;
            graph = new Microsoft.Msagl.Drawing.Graph("graph");
            graphEdges = new List<Dictionary<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>>>();
            accState = new List<bool>();
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            addNode();
            addEdge(0, "e", 0);
            refreshGraph1();
            viewer.Graph = graph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);

            
            nbState2 = 0;
            accState2 = new List<bool>();
            /*
            viewer2 = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            addNode2();
            addEdge2(0, 'e', 0);
            viewer2.Graph = graph2;
            viewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Controls.Add(viewer2);
            */
            createDFA();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            addNode();
        }

        private void addNode()
        {
            Dictionary<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>> newNodeEdges = new Dictionary<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>>();
            graphEdges.Add(newNodeEdges);
            accState.Add(false);
            comboBox1.Items.Add(nbState.ToString());
            comboBox4.Items.Add(nbState.ToString());
            comboBox5.Items.Add(nbState.ToString());
            nbState++;
        }

        private void addNode2()
        {
            Dictionary<char, KeyValuePair<int, Microsoft.Msagl.Drawing.Edge>> newNodeEdges = new Dictionary<char, KeyValuePair<int, Microsoft.Msagl.Drawing.Edge>>();
            graphEdges2.Add(newNodeEdges);
            accState2.Add(false);
            nbState2++;
        }

        private void addEdge(int sourceId, string val, int targetId)
        {
            if (!graphEdges[sourceId].ContainsKey(targetId))
            {
                graphEdges[sourceId].Add(targetId, new Dictionary<char, Microsoft.Msagl.Drawing.Edge>());
            }
            if (!graphEdges[sourceId][targetId].ContainsKey(val[0]))
            {
                graphEdges[sourceId][targetId].Add(val[0], graph.AddEdge(sourceId.ToString(), val, targetId.ToString()));
            }
        }

        private void addEdge2(int sourceId, char val, int targetId)
        {
            if (!graphEdges2[sourceId].ContainsKey(val))
            {
                graphEdges2[sourceId].Add(val, new KeyValuePair<int, Microsoft.Msagl.Drawing.Edge>(
                    targetId, graph2.AddEdge(sourceId.ToString(), val.ToString(), targetId.ToString())));
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            textBox2.Text = "";
            panel1.Controls.Remove(viewer);

            string spaced = new string(textBox1.Text.ToCharArray().Where(c=>!Char.IsWhiteSpace(c)).ToArray());
            List<char> final = new List<char>();
            final.Add(spaced[0]);
            for(int i=1; i<spaced.Length; i++)
            {
                if(spaced[i] == '*' && spaced[i-1] != ')')
                {
                    int cnt = final.Count();
                    char last = final[cnt - 1];
                    final[cnt - 1] = '(';
                    final.Add(last);
                    final.Add(')');
                    final.Add('*');
                }
                else
                {
                    final.Add(spaced[i]);
                }
            }
            string finalStr = new string(final.ToArray());

            graph = new Microsoft.Msagl.Drawing.Graph("graph");
            graphEdges = new List<Dictionary<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>>>();
            nbState = 0;
            accState = new List<bool>();

            Stack<int> stack1 = new Stack<int>();
            Stack<int> stack2 = new Stack<int>();
            addNode();
            stack1.Push(nbState-1);
            stack2.Push(nbState - 1);

            int openBracketState = 0;
            for(int i=0; i<finalStr.Length; i++)
            {
                if(finalStr[i] == '(')
                {
                    addNode();
                    int source = stack2.Pop();
                    addEdge(source, "e", nbState - 1);
                    stack1.Push(nbState - 1);
                    stack2.Push(-1);
                    stack2.Push(nbState - 1);

                }else if(finalStr[i] == '+')
                {
                    int top = stack1.Peek();
                    stack2.Push(top);
                }else if(finalStr[i] == ')')
                {
                    int top = stack2.Pop();
                    addNode();
                    while (top >= 0)
                    {
                        addEdge(top, "e", nbState - 1);
                        top = stack2.Pop();
                    }
                    openBracketState = stack1.Pop();
                    stack2.Push(nbState - 1);
                }else if(finalStr[i] == '*')
                {
                    int top = stack2.Pop();
                    addEdge(top, "e", openBracketState);
                    stack2.Push(openBracketState);
                }else
                {
                    int top = stack2.Pop();
                    addNode();
                    addEdge(top, finalStr[i].ToString(), nbState - 1);
                    stack2.Push(nbState - 1);
                }
            }
            while (stack2.Count > 0)
            {
                int top = stack2.Pop();
                accState[top] = true;
                graph.FindNode(top.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
            }

            refreshGraph1();

            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            viewer.Graph = graph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);


            createDFA();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(comboBox4.Text.Length>0 && comboBox5.Text.Length > 0 && comboBox6.Text.Length > 0)
            {
                int sourceId = int.Parse(comboBox4.Text);
                int targetId = int.Parse(comboBox5.Text);
                panel1.Controls.Remove(viewer);

                addEdge(sourceId, comboBox6.Text, targetId);
                refreshGraph1();
                
                viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
                viewer.Graph = graph;
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                panel1.Controls.Add(viewer);

                createDFA();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Remove(viewer);

            if(comboBox1.Text.Length > 0)
            {
                if (accState[int.Parse(comboBox1.Text)])
                {
                    graph.FindNode(comboBox1.Text).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                }
                else
                {
                    graph.FindNode(comboBox1.Text).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                }
                accState[int.Parse(comboBox1.Text)] = !accState[int.Parse(comboBox1.Text)];
            }

            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            viewer.Graph = graph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);

            createDFA();
        }

        private void resetColor1()
        {
            for (int i = 0; i < nbState; i++)
            {
                graph.FindNode(i.ToString()).Attr.FillColor = accState[i] ? Microsoft.Msagl.Drawing.Color.Yellow : Microsoft.Msagl.Drawing.Color.White;
                graph.FindNode(i.ToString()).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                foreach (KeyValuePair<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>> e in graphEdges[i])
                {
                    foreach (KeyValuePair<char, Microsoft.Msagl.Drawing.Edge> edge in e.Value)
                    {
                        edge.Value.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    }
                }
            }
        }

        private void resetColor2() {
            for (int i = 0; i < nbState2; i++)
            {
                graph2.FindNode(i.ToString()).Attr.FillColor = accState2[i] ? Microsoft.Msagl.Drawing.Color.Yellow : Microsoft.Msagl.Drawing.Color.White;
                graph2.FindNode(i.ToString()).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                foreach (KeyValuePair<char, KeyValuePair<int, Microsoft.Msagl.Drawing.Edge>> e in graphEdges2[i])
                {
                    e.Value.Value.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                }
            }
        }

        private void refreshGraph1()
        {
            resetColor1();
            HashSet<int> now = findSingleEClosure(0, true);
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                HashSet<int> next = new HashSet<int>();
                foreach (int j in now)
                {
                    next.UnionWith(findSingleTableNFA(j, textBox2.Text[i], true));
                }
                now = next;
            }
            foreach (int i in now)
            {
                graph.FindNode(i.ToString()).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
            }
        }

        private void refreshGraph2() { 
            resetColor2();
            int now2 = 0;
            for (int i = 0; i < textBox2.Text.Length; i++)
            { 
                int next = graphEdges2[now2][textBox2.Text[i]].Key;
                graphEdges2[now2][textBox2.Text[i]].Value.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                now2 = next;
            }
            graph2.FindNode(now2.ToString()).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            panel1.Controls.Remove(viewer);
            refreshGraph1();
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            viewer.Graph = graph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);

            panel2.Controls.Remove(viewer2);
            refreshGraph2();
            viewer2 = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            viewer2.Graph = graph2;
            viewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Controls.Add(viewer2);
        }

        private HashSet<int> findSingleEClosure(int i, bool colored)
        {
            HashSet<int> toAdd = new HashSet<int>();
            bool[] visited = new bool[nbState];
            for (int j = 0; j < nbState; j++)
            {
                visited[j] = false;
            }
            Stack<int> dfs = new Stack<int>();
            dfs.Push(i);
            visited[i] = true;
            while (dfs.Count > 0)
            {
                int expand = dfs.Pop();
                foreach (KeyValuePair<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>> e in graphEdges[expand])
                {
                    if (e.Value.ContainsKey('e'))
                    {
                        if (colored) { e.Value['e'].Attr.Color = Microsoft.Msagl.Drawing.Color.Red; }
                        if (!visited[e.Key])
                        {
                            visited[e.Key] = true;
                            dfs.Push(e.Key);
                        }
                    }
                }
                toAdd.Add(expand);
            }
            return toAdd;
        }

        private List<HashSet<int>> findEClosure()
        {
            List<HashSet<int>> eClosure = new List<HashSet<int>>();
            //DFS untuk mencari eClosure tiap node
            for (int i = 0; i < nbState; i++)
            {
                HashSet<int> toAdd = findSingleEClosure(i, false);
                eClosure.Add(toAdd);
            }
            return eClosure;
        }

        private HashSet<int> findSingleTableNFA(int source, char c, bool colored)
        {
            HashSet<int> toReturn = new HashSet<int>();
            foreach (KeyValuePair<int, Dictionary<char, Microsoft.Msagl.Drawing.Edge>> e in graphEdges[source])
            {
                if (e.Value.ContainsKey(c))
                {
                    e.Value[c].Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    toReturn.UnionWith(findSingleEClosure(e.Key, true));
                }
            }
            return toReturn;
        }

        private void createDFA()
        {
            List<HashSet<int>> eClosure = findEClosure();

            //Bikin tabel NFA
            List<Dictionary<char, HashSet<int>>> tableNFA = new List<Dictionary<char, HashSet<int>>>();
            for(int i=0; i<nbState; i++)
            {
                Dictionary<char, HashSet<int>> toAdd = new Dictionary<char, HashSet<int>>();
                for(int j=1; j<characters.Length; j++)
                {
                    HashSet<int> set = new HashSet<int>();
                    foreach(int k in eClosure[i])
                    {
                        set.UnionWith(findSingleTableNFA(k, characters[j], false));
                    }
                    toAdd.Add(characters[j], set);
                }
                tableNFA.Add(toAdd);
            }

            panel2.Controls.Remove(viewer2);
            //Bikin DFA
            graph2 = new Microsoft.Msagl.Drawing.Graph();
            graphEdges2 = new List<Dictionary<char, KeyValuePair<int, Microsoft.Msagl.Drawing.Edge>>>();
            nbState2 = 0;
            accState2 = new List<bool>();
            List<HashSet<int>> nodeID = new List<HashSet<int>>();
            Stack<HashSet<int>> dfsDFA = new Stack<HashSet<int>>();
            Stack<int> dfsDFAID = new Stack<int>();

            addNode2();
            HashSet<int> start = new HashSet<int>();
            start.Add(0);
            nodeID.Add(start);
            dfsDFA.Push(start);
            dfsDFAID.Push(0);

            while (dfsDFA.Count > 0)
            {
                HashSet<int> top = dfsDFA.Pop();
                int idxTop = dfsDFAID.Pop();
                for (int i=1; i<characters.Length; i++)
                {
                    HashSet<int> toPush = new HashSet<int>();
                    foreach (int e in top)
                    {
                        toPush.UnionWith(tableNFA[e][characters[i]]);
                    }
                    int idx = -1;
                    for(int j=0; j<nbState2; j++)
                    {
                        if (nodeID[j].SetEquals(toPush))
                        {
                            idx = j;
                        }
                    }
                    if (idx < 0) //Tambah node baru
                    {
                        addNode2();
                        idx = nbState2 - 1;
                        nodeID.Add(toPush);
                        addEdge2(idxTop, characters[i], idx);
                        //Cek apakah accepted state
                        foreach (int j in toPush)
                        {
                            if (accState[j])
                            {
                                accState2[idx] = true;
                            }
                        }
                        dfsDFA.Push(toPush);
                        dfsDFAID.Push(idx);
                    }
                    else
                    {
                        addEdge2(idxTop, characters[i], idx);
                    }
                }
            }
            foreach(int i in eClosure[0])
            {
                accState2[0] = accState2[0] || accState[i];
            }
            refreshGraph2();
            //Yey kelar
            viewer2 = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            viewer2.Graph = graph2;
            viewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Controls.Add(viewer2);
        }
    }
}

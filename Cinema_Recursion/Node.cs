using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Recursion
{
    class Node
    {
        public int EmptyPlace;
        public List<int> CurrentBoxes;
        public static List<int> ListOfBoxes;
        List<Node> Next;


        public Node(int emptyPlace, List<int> currentBoxes = null)
        {
            EmptyPlace = emptyPlace;
            if (currentBoxes == null)
            {
                CurrentBoxes = new List<int>();
            }
            else
            {
                CurrentBoxes = currentBoxes;
            }
            Next = new List<Node>();
        }

        public void CreateGraph()
        {
            foreach(int box in ListOfBoxes)
            {
                if (box <= EmptyPlace)
                {
                    List<int> tmp = new List<int>();
                    foreach(int i in CurrentBoxes)
                    {
                        tmp.Add(i);
                    }
                    tmp.Add(box);
                    Node node = new Node(EmptyPlace - box, tmp);

                    Next.Add(node);
                    node.CreateGraph();
                }
            }
        }

        public void WriteAllEnds()
        {
            if (Next.Count == 0)
            {
                foreach (int i in CurrentBoxes)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
            else
            {
                foreach(Node q in Next)
                {
                    q.WriteAllEnds();
                }
            }
        }

        public Result GetResult()
        {
            if (Next.Count == 0)
            {
                return new Result(EmptyPlace, CurrentBoxes);
            }
            else
            {
                List<Result> results = new List<Result>();
                foreach (Node q in Next)
                {
                    results.Add(q.GetResult());
                }

                Result minResult = results[0];
                foreach(Result result in results)
                {
                    if(result.EmptyPlace <
                        minResult.EmptyPlace)
                    {
                        minResult = result;
                    }
                }
                return minResult;
            }
        }
    }

    public class Result
    {
        public int EmptyPlace;
        public List<int> CurrentBoxes;

        public Result(int emptyPlace, List<int> currentBoxes)
        {
            EmptyPlace = emptyPlace;
            CurrentBoxes = currentBoxes;
        }







    }
}

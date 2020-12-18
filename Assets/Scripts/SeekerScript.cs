using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SeekerScript : MonoBehaviour
{
    Grid GridHolder;
    public Transform startPos;
    public Transform finishPos;

    private void Awake()
    {
        GridHolder = GetComponent<Grid>();
        //StartCoroutine(hold());
    }
    IEnumerator hold()
    {
        yield return new WaitForSeconds(1.0f);
        moveObject();
    }
    private void Update()
    {
        FindPath(startPos.position, finishPos.position);
        StartCoroutine(hold());
        

    }

    void FindPath(Vector3 startNode, Vector3 finishNode)
    {
        Node sn = GridHolder.NodeFromWorldPoint(startNode);
        Node fn = GridHolder.NodeFromWorldPoint(finishNode);

        List<Node> NotSeen = new List<Node>();
        HashSet<Node> Seen = new HashSet<Node>();

        NotSeen.Add(sn);

        while (NotSeen.Count > 0)
        {
            Node CurrentNode = NotSeen[0];
            for (int i = 1; i < NotSeen.Count; i++)
            {
                if (NotSeen[i].FCost < CurrentNode.FCost || NotSeen[i].FCost == CurrentNode.FCost && NotSeen[i].ihCost < CurrentNode.ihCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {
                    CurrentNode = NotSeen[i];
                }
            }
            NotSeen.Remove(CurrentNode);
            Seen.Add(CurrentNode);

            if (CurrentNode == fn)
            {
                GetFinalPath(sn, fn);
            }

            foreach (Node n in GridHolder.GetNeighboringNodes(CurrentNode))
            {
                if (!n.objectWall || Seen.Contains(n))
                {
                    continue;
                }
                int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, n);

                if (MoveCost < n.igCost || !NotSeen.Contains(n))
                {
                    n.igCost = MoveCost;
                    n.ihCost = GetManhattenDistance(n, fn);
                    n.parent = CurrentNode;

                    if (!NotSeen.Contains(n))
                    {
                        NotSeen.Add(n);
                    }
                }
            }

        }
    }



    void GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while (CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.parent;
        }

        FinalPath.Reverse();

        GridHolder.FinalPath = FinalPath;

    }
    //GameObject seeker = GameObject.FindGameObjectWithTag("SeekerEnemy");
    public Transform Player;
    void moveObject()
    {
        
        List<Node> holder = GridHolder.FinalPath;
        int index = 0;
        foreach(Node n in holder)
        {
            Vector3 tar = n.pos;
            if(Vector3.Distance(transform.position, tar) > 0.01f)
            {
                Vector3 moveDir = (tar - transform.position).normalized;
                float dist = Vector3.Distance(transform.position, tar);
                transform.position = transform.position + moveDir * 1.0f * Time.deltaTime;

            }
            else
            {
                index++;
                if(index >= holder.Count)
                {
                    
                }
            }
        }

    }
    public List<Vector3> getWalkableList(Vector3 start, Vector3 end)
    {
        //GridHolder.NodeFromWorldPoint(start);
        //GridHolder.NodeFromWorldPoint(end);

        //List<Node> p = FindPath(startPos.position, finishPos.position);

        return null;
    }
    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);//x1-x2
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);//y1-y2

        return ix + iy;//Return the sum
    }
}

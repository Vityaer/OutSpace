using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PathFinder{
	[System.Serializable]
	public class Node{
		[SerializeField]
		private string name;
		public string Name{get => name;}
		private Transform point;
		public Transform Point {get => point;}
		[SerializeField]
		private float minDist;
		public float MinDist {get => minDist;}
		[SerializeField]
		private string nameNeighbour;
		private Node previousNeighbour;
		public Node Neighbour {get => previousNeighbour;}
		public Node(Transform Point){
			this.point   = Point;
			this.name    = Point.name;
			this.minDist = Mathf.Infinity;
		}
		public void CheckNeighbour(Node node){
			float dist = sqrMagnitude(node.Point.position,Point.position) + node.MinDist;
			if(minDist > dist){
				minDist = dist;
				previousNeighbour = node;
				nameNeighbour = previousNeighbour.name;
			}
			float sqrMagnitude(Vector2 a, Vector2 b){
				return((b.x - a.x)*(b.x - a.x) + (b.y - a.y)*(b.y - a.y) );
			}
		}
		public void StartDist(){
			minDist = 0f;
		}
		public void Refresh(){
			minDist = Mathf.Infinity;
			nameNeighbour = "";
			previousNeighbour = null;
		}
	}
	[System.Serializable]
	public class Relation{
		[SerializeField]
		private Node leftNode;
		public Node LeftNode{get => leftNode;}
		[SerializeField]
		private Node rightNode;
		public Node RightNode{get => rightNode;}
		[SerializeField]
		private float dist;
		public float Dist {get => dist;}
		public Relation(Node leftNode, Node rightNode){
			this.leftNode  = leftNode;
			this.rightNode = rightNode;
			this.dist      = Vector3.Distance(leftNode.Point.position, rightNode.Point.position);
		}
		public void Refresh(){
			this.leftNode.Refresh();
			this.rightNode.Refresh();
		}
		public bool Check(Node firstNode, Node secondNode){
			bool result = false;
			if((firstNode.Point == leftNode.Point) || (firstNode.Point == rightNode.Point)){
				if((secondNode.Point == leftNode.Point) || (secondNode.Point == rightNode.Point)){
					result = true;
				}
			}
			return result;
		}
	}
	public class PathFinderCore : MonoBehaviour{
		public Transform points;
		public bool showGizmos = true;
		void Awake(){
			showGizmos = false;
			CheckList();
			Scan();
		}
//Gizmos
		void OnDrawGizmos() {
			if(showGizmos){
				if(points){
					CheckList();
					ShowIconPoints();
					ShowRelation();	
				}
			}
  		}
    	
    	void ShowIconPoints(){
    		foreach(Node child in listPoint){
        		Gizmos.DrawIcon(child.Point.position, "Light Gizmo.tiff", true);
        		Handles.Label(child.Point.position, child.Point.name);
			}
    	}
    	void ShowRelation(){
			bool validate = true;
    		for(int i = 0; i < relation.Count; i++){
    			if((relation[i].LeftNode.Point != null) && (relation[i].LeftNode.Point != null)){
					CreateLine(relation[i]);	
    			}else{
    				validate = false;
    			}
			}
			if(!validate){
				ClearRelation();
				Scan();
			}
    	}
    	void CreateLine(Relation relation){
			Gizmos.DrawLine(relation.LeftNode.Point.position, relation.RightNode.Point.position);
    	}
//End Gizmos


    	public List<Node> listPoint = new List<Node>();
    	public List<Relation> relation = new List<Relation>(); 
    	public void CheckList(){
			listPoint.Clear();
			foreach(Transform child in points){
				listPoint.Add(new Node(child));
			}
    	}
    	public Vector2 deltaX;
    	public Vector2 deltaY;
    	public void Scan(){
    		if(points){
	    		Debug.Log("Scaning...");
    			bool check = false;
    			for(int i=0; i<listPoint.Count - 1; i++){
    				for(int j=i+1; j < listPoint.Count; j++){
    					if(Distance(listPoint[i].Point, listPoint[j].Point)){
	    					check = false;
    						for(int k = 0; k < relation.Count; k++){
    							if(relation[k].Check(listPoint[i], listPoint[j])){
									check = true;
									break;					
								}
    						}
    						if(!check)
	    						relation.Add(new Relation(listPoint[i], listPoint[j]));
    					}
    				}
    			}
    		}
    	}

    	bool Distance(Transform start, Transform finish){
	    	Vector2 dir;
    		bool result = false;
    		string answer = "";
    		if((( Mathf.Abs(start.position.x - finish.position.x) < deltaX.y ) && ( Mathf.Abs(start.position.y - finish.position.y) < deltaY.x )) || (( Mathf.Abs(start.position.x - finish.position.x) < deltaX.x ) && ( Mathf.Abs(start.position.y - finish.position.y) < deltaY.y ))){
		    	result = true;
		    	dir.x = finish.position.x - start.position.x; 
		    	dir.y = finish.position.y - start.position.y;
		    	result = !MyPhysics2D.RaycastFindLayer(start.position, dir, dir.magnitude, 9);
    		}
    		return result; 
    	}
    	

//API
    	public Node GetRandomNearPointNeigbours(Vector3 target){
    		Node nearNode = GetNearPointToTarget(target, false);
    		List<Node> neighbours = new List<Node>();
    		for(int i=0; i < relation.Count; i++){
				if(relation[i].LeftNode.Point == nearNode.Point){
					neighbours.Add(relation[i].RightNode);
				}else if(relation[i].RightNode.Point == nearNode.Point){
					neighbours.Add(relation[i].LeftNode);
				}
			}
			return neighbours[ UnityEngine.Random.Range(0, neighbours.Count) ];
    	}
		public Node GetNearPointToTarget(Vector3 target){
			return GetNearPointToTarget(target, false);
		}
		public Node GetNearPointToTarget(Vector3 target, bool safe){
	    	Vector2 dir;
			float dist = Mathf.Infinity;
			Node result = null;
			foreach (Node node in listPoint){
				if(Vector3.Distance(node.Point.position, target) < dist){
					dir = target - node.Point.position;
					if(MyPhysics2D.RaycastFindLayer(node.Point.position, dir, dir.magnitude, 9) == safe){
						dist = Vector3.Distance(node.Point.position, target);
						result = node;
					} 
				}
			}
			return result;
		}
		public Node GetNearSafePoint(Transform enemy, Transform aggressor, float wEnemy = -0.5f, float wAggressor = 0.3f ){
	    	wEnemy     =   UnityEngine.Random.Range(0.1f, 1f);
	    	wAggressor = - UnityEngine.Random.Range(1f,   2f);
	    	Vector2 dirEnemy, dirAggressor;
			float MinMax = Mathf.Infinity, curDist;
			Node result = null;
			foreach (Node node in listPoint){
				dirAggressor = aggressor.position - node.Point.position;
				dirEnemy     = enemy.position     - node.Point.position;
				if(MyPhysics2D.RaycastFindLayer(node.Point.position, dirAggressor, dirAggressor.magnitude, 9) == true){
					curDist = dirEnemy.magnitude * wEnemy + dirAggressor.magnitude * wAggressor;
					if(curDist < MinMax){
						MinMax = curDist;
						result = node;
					}
				}
			}
			return result;
		}
		public void NearPoint(){
			GetNearPointToTarget(GameObject.Find("Player").GetComponent<Transform>().position);
		}
//Core	
	//Find way	
		public void FindWay(Node startNode, Node finishNode, List<Node> result){
			RefreshNodes();
			Queue<Node> checkNodes   = new Queue<Node>();
			List<Node> visibledNode = new List<Node>();
			startNode.StartDist();
			checkNodes.Enqueue(startNode);
			Node currentNode;
			while (checkNodes.Count > 0){
				currentNode = checkNodes.Dequeue();
				if(!CheckVisibledNode(currentNode)){
					for(int i=0; i < relation.Count; i++){
						if(relation[i].LeftNode.Point == currentNode.Point){
							relation[i].RightNode.CheckNeighbour(currentNode);
							if(relation[i].RightNode.MinDist < finishNode.MinDist){
								if(!CheckVisibledNode(relation[i].RightNode))
									checkNodes.Enqueue(relation[i].RightNode);
							}
						}else if(relation[i].RightNode.Point == currentNode.Point){
							relation[i].LeftNode.CheckNeighbour(currentNode);
							if(relation[i].LeftNode.MinDist < finishNode.MinDist){
								if(!CheckVisibledNode(relation[i].LeftNode))
									checkNodes.Enqueue(relation[i].LeftNode);
							}
						}
					}
					visibledNode.Add(currentNode);
				}
			}
			bool end = false;
			result.Add(finishNode);
			while((!end)){
				if(finishNode.Neighbour != null){
					result.Add(finishNode.Neighbour);
					finishNode = finishNode.Neighbour;
				}else{
					end = true; 
				}
			}
			result.Reverse();
			bool CheckVisibledNode(Node node){
				bool answer = false;
				for(int i=0; i<visibledNode.Count; i++)
					if(visibledNode[i].Point == node.Point){
						answer = true; 
						break;
					}
				return answer;	
			}
		}
//EndCode
//Help function		
		void RefreshNodes(){
			foreach(Relation rel in relation){
				rel.Refresh();
			}
		}
		public void ClearRelation(){
			relation.Clear();
		}
		public Transform oneNode, twoNode;
		public void AddRelation(){
			Node firstNode  = CheckNode(oneNode);
			Node secondNode = CheckNode(twoNode);
			bool check = false;
			foreach (Relation rel in relation) {
				if(rel.Check(firstNode, secondNode)){
					check = true;
					break;					
				}
			}
			if(!check){
				relation.Add(new Relation(firstNode, secondNode));
				Debug.Log("успешно добавлено!");
			}else{
				Debug.Log("ты чё дурак, блять!?");
			}
			Node CheckNode(Transform checkTransform){
				bool result = true;
				Node answerNode = null;
				foreach (Node node in listPoint){
					if(checkTransform == node.Point){
						result = false;
						answerNode = node; 
						break;
					}
				}
				if(result){
					answerNode = new Node(checkTransform);
					listPoint.Add(answerNode);
				}
				return answerNode;
			}
		}
		public void RemoveRelation(){
			Node firstNode = new Node(oneNode);
			Node secondNode = new Node(twoNode);
			int result = -1;
			for(int i=0; i<relation.Count; i++){
				if(relation[i].Check(firstNode, secondNode)){
					result = i;
					break;
				}
			}
			if(result > 0){
				relation.RemoveAt(result);
				Debug.Log("Успешно удалено!");
			}else{
				Debug.Log("такой связи и так не было");
			}
		}
	}
}  
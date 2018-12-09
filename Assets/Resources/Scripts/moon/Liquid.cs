using UnityEngine;
using System.Collections;

public class Liquid : MonoBehaviour {

    //Our renderer that'll make the top of the water visible
    LineRenderer Body;

    //Our physics arrays
    float[] xpositions;
    float[] ypositions;
    float[] velocities;
    float[] accelerations;

    //Our meshes and colliders
    GameObject[] meshobjects;
    GameObject[] colliders;
    Mesh[] meshes;

    //The material we're using for the top of the water
    public Material mat;

    //The GameObject we're using for a mesh
    public GameObject watermesh;

    //All our constants
    const float springconstant = 0.02f;
    const float damping = 0.04f;
    const float spread = 0.05f;
    const float z = -1f;

    //The properties of our liquid
    float baseheight;
    float left;
    float bottom;

	//Percent of filling for the moon
	public float filling;
	float prevFilling;
    

    void Start()
    {
        //Spawning our liquid
		SpawnLiquid ( -2.5f, 4, 0, -3 );

		StartCoroutine ( TideEffect ( 0 ) );

		prevFilling = filling;
    }

    
    public void Splash(float xpos, float velocity)
    {
        //If the position is within the bounds of the water:
        if (xpos >= xpositions[0] && xpos <= xpositions[xpositions.Length-1])
        {
            //Offset the x position to be the distance from the left side
            xpos -= xpositions[0];

            //Find which spring we're touching
            int index = Mathf.RoundToInt((xpositions.Length-1)*(xpos / (xpositions[xpositions.Length-1] - xpositions[0])));

            //Add the velocity of the falling object to the spring
            velocities[index] += velocity;
        }
    }

    public void SpawnLiquid(float Left, float Width, float Top, float Bottom)
    {
        //Calculating the number of edges and nodes we have
        int edgecount = Mathf.RoundToInt(Width) * 5;
        int nodecount = edgecount + 1;
        
        //Add our line renderer and set it up:
        Body = gameObject.AddComponent<LineRenderer>();
        Body.material = mat;
        Body.material.renderQueue = 1000;
        Body.SetVertexCount(nodecount);
        Body.SetWidth(0.1f, 0.1f);

        //Declare our physics arrays
        xpositions = new float[nodecount];
        ypositions = new float[nodecount];
        velocities = new float[nodecount];
        accelerations = new float[nodecount];
        
        //Declare our mesh arrays
        meshobjects = new GameObject[edgecount];
        meshes = new Mesh[edgecount];
        colliders = new GameObject[edgecount];

        //Set our variables
        baseheight = Top;
        bottom = Bottom;
        left = Left;

        //For each node, set the line renderer and our physics arrays
        for (int i = 0; i < nodecount; i++)
        {
            ypositions[i] = Top;
            xpositions[i] = Left + Width * i / edgecount;
            Body.SetPosition(i, new Vector3(xpositions[i], Top, z));
            accelerations[i] = 0;
            velocities[i] = 0;
        }

        //Setting the meshes now:
        for (int i = 0; i < edgecount; i++)
        {
            //Make the mesh
            meshes[i] = new Mesh();

            //Create the corners of the mesh
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
            Vertices[2] = new Vector3(xpositions[i], bottom, z);
            Vertices[3] = new Vector3(xpositions[i+1], bottom, z);

            //Set the UVs of the texture
            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            //Set where the triangles should be.
            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0};

            //Add all this data to the mesh.
            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            //Create a holder for the mesh, set it to be the manager's child
            meshobjects[i] = Instantiate(watermesh,Vector3.zero,Quaternion.identity) as GameObject;
            meshobjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshobjects[i].transform.parent = transform;

            //Create our colliders, set them be our child
            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider>();
            colliders[i].transform.parent = transform;

            //Set the position and scale to the correct dimensions
            colliders[i].transform.position = new Vector3(Left + Width * (i + 0.5f) / edgecount, Top - 0.5f, 0);
            colliders[i].transform.localScale = new Vector3(Width / edgecount, 1, 1);

            //Add a WaterDetector and make sure they're triggers
            colliders[i].GetComponent<BoxCollider>().isTrigger = true;
            colliders[i].AddComponent<LiquidDetector>();

        }   
    }

    //Same as the code from in the meshes before, set the new mesh positions
    void UpdateMeshes()
    {
        for (int i = 0; i < meshes.Length; i++)
        {

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i+1], ypositions[i+1], z);
            Vertices[2] = new Vector3(xpositions[i], bottom, z);
            Vertices[3] = new Vector3(xpositions[i+1], bottom, z);

            meshes[i].vertices = Vertices;
        }
    }

	void UpdateLevel ( ) {
		float diff = filling -  prevFilling;

		baseheight = diff;
		for ( int i = 0; i < ypositions.Length; i++ ) {
			ypositions[i] += diff;
		}

		prevFilling = filling;

		transform.position = new Vector3 ( 0, filling, 10 );
	}

    //Called regularly by Unity
    void FixedUpdate()
    {
        //Here we use the Euler method to handle all the physics of our springs:
        for (int i = 0; i < xpositions.Length ; i++)
        {
            float force = springconstant * (ypositions[i] - baseheight) + velocities[i]*damping ;
            accelerations[i] = -force;
            ypositions[i] += velocities[i];
            velocities[i] += accelerations[i];
            Body.SetPosition(i, new Vector3(xpositions[i], filling + ypositions[i], z));
        }

        //Now we store the difference in heights:
        float[] leftDeltas = new float[xpositions.Length];
        float[] rightDeltas = new float[xpositions.Length];

        //We make 8 small passes for fluidity:
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < xpositions.Length; i++)
            {
                //We check the heights of the nearby nodes, adjust velocities accordingly, record the height differences
                if (i > 0)
                {
                    leftDeltas[i] = spread * (ypositions[i] - ypositions[i-1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < xpositions.Length - 1)
                {
                    rightDeltas[i] = spread * (ypositions[i] - ypositions[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }

            //Now we apply a difference in position
            for (int i = 0; i < xpositions.Length; i++)
            {
                if (i > 0)
                    ypositions[i-1] += leftDeltas[i];
                if (i < xpositions.Length - 1)
                    ypositions[i + 1] += rightDeltas[i];
            }
        }
        
		//Finally we update the meshes to reflect this and the level of the liquid
		UpdateMeshes ( );
		UpdateLevel ( );
	}

	//Simulate a tide effect
	IEnumerator TideEffect ( int idx ) {
		yield return new WaitForSeconds ( Random.Range ( 1, 2 ) * Random.value );
		
		velocities[idx] = Random.Range ( -1, 1 ) * Random.value / 3;

		int nextIdx = idx == 0 ? velocities.Length - 1 : 0;
		
		StartCoroutine ( TideEffect ( nextIdx ) );
	}


}

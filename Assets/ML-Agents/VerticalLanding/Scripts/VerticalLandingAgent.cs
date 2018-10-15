using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VerticalLandingAgent : Agent
{
    private int fuelLevel;
    private Rigidbody rigidBody;
    private Transform target;

    /// <summary>
    /// Specifies the agent behavior at every step
    /// based on the provided action.
    /// </summary>
    ///
    /// <param name="vectorAction">
    /// Vector action.
    /// Note that for discrete actions, the provided array will be of length 1.
    /// </param>
    ///
    /// <param name="textAction">
    /// Text action.
    /// </param>
    ///
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (vectorAction[0] > 0 && fuelLevel > 0)
        {
            rigidBody.AddForce(new Vector3(0, vectorAction[0], 0));
            fuelLevel -= 1;

            // GameObject.Find("Thrust").GetComponent<Text>().text =
            // string.Format("Thrust: {0}", vectorAction[0].ToString("F2"));
        }
        // else
        // {
        //     GameObject.Find("Thrust").GetComponent<Text>().text = "Thrust: 0";
        // }

        float distanceToTarget = Vector3.Distance(
            this.transform.position, target.position);

        // GameObject.Find("Fuel").GetComponent<Slider>().value = fuelLevel;

        // GameObject.Find("Distance").GetComponent<Text>().text =
        // string.Format("Distance: {0}", distanceToTarget.ToString("F2"));

        // GameObject.Find("Reward").GetComponent<Text>().text =
        // string.Format("Reward: {0}", GetCumulativeReward());

        // GameObject.Find("Velocity").GetComponent<Text>().text =
        // string.Format("Velocity: {0}", rigidBody.velocity.y.ToString("F2"));
    }

    /// <summary>
    /// Specifies the agent behavior when done and
    /// <see cref="AgentParameters.resetOnDone"/> is false.
    ///
    /// This method can be used to remove the agent from the scene.
    /// </summary>
    ///
    public override void AgentOnDone()
    {

    }

    /// <summary>
    /// Specifies the agent behavior when being reset,
    /// which can be due to the agent or Academy being done
    /// (i.e. completion of local or global episode).
    /// </summary>
    ///
    public override void AgentReset()
    {
        this.transform.position = new Vector3(0, 10, 0);
        this.rigidBody.angularVelocity = Vector3.zero;
        this.rigidBody.velocity = Vector3.zero;
        fuelLevel = 100;
    }

    /// <summary>
    /// Collects the (vector, visual, text) observations of the agent.
    /// The agent observation describes the current environment from the
    /// perspective of the agent.
    /// </summary>
    ///
    /// <remarks>
    /// Simply, an agents observation is any environment information that helps
    /// the Agent acheive its goal. For example, for a fighting Agent, its
    /// observation could include distances to friends or enemies, or the
    /// current level of ammunition at its disposal.
    /// Recall that an Agent may attach vector, visual or textual observations.
    /// Vector observations are added by calling the provided helper methods:
    ///     - <see cref="AddVectorObs(int)"/>
    ///     - <see cref="AddVectorObs(float)"/>
    ///     - <see cref="AddVectorObs(Vector3)"/>
    ///     - <see cref="AddVectorObs(Vector2)"/>
    ///     - <see cref="AddVectorObs(float[])"/>
    ///     - <see cref="AddVectorObs(List{float})"/>
    ///     - <see cref="AddVectorObs(Quaternion)"/>
    /// Depending on your environment, any combination of these helpers can
    /// be used. They just need to be used in the exact same order each time
    /// this method is called and the resulting size of the vector observation
    /// needs to match the observationSize attribute of the linked Brain.
    /// Visual observations are implicitly added from the cameras attached to
    /// the Agent.
    /// Lastly, textual observations are added using
    /// <see cref="SetTextObs(string)"/>.
    /// </remarks>
    ///
    public override void CollectObservations()
    {
        float distanceToTarget = Vector3.Distance(
            this.transform.position, target.position);

        AddVectorObs(distanceToTarget);
        AddVectorObs(rigidBody.velocity.y);
    }

    /// <summary>
    /// Initializes the agent, called once when the agent is enabled. Can be
    /// left empty if there is no special, unique set-up behavior for the
    /// agent.
    /// </summary>
    ///
    /// <remarks>
    /// One sample use is to store local references to other objects in the
    /// scene which would facilitate computing this agents observation.
    /// </remarks>
    ///
    public override void InitializeAgent()
    {
        fuelLevel = 100;

        rigidBody = GetComponent<Rigidbody>();

        target = GameObject.Find("LandingPlatform").transform;

        var weight = rigidBody.mass * Physics.gravity.y;

        // GameObject.Find("Mass").GetComponent<Text>().text =
        // string.Format("Mass: {0}", rigidBody.mass);

        // GameObject.Find("Weight").GetComponent<Text>().text =
        // string.Format("Weight: {0}", weight);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            AddReward(-1f);
        }
        else
        {
            AddReward(1f);
        }

        GameObject.Find("Academy").GetComponent<Academy>().Done();
    }
}

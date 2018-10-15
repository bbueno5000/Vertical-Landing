using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VerticalLandingAcademy : Academy
{
    /// <summary>
    /// Specifies the academy behavior when being reset (i.e. at the completion
    /// of a global episode).
    /// </summary>
    ///
    public override void AcademyReset()
    {
        // GameObject.Find("TextEpisode").GetComponent<Text>().text =
        // "Episode: " + GetEpisodeCount().ToString("F2");
    }

    /// <summary>
    /// Specifies the academy behavior at every step of the environment.
    /// </summary>
    ///
    public override void AcademyStep()
    {
        // GameObject.Find("TextSteps").GetComponent<Text>().text =
        // "Steps: " + GetStepCount().ToString("F2");
    }

    /// <summary>
    /// Initializes the academy and environment. Called during the waking-up
    /// phase of the environment before any of the scene objects/agents have
    /// been initialized.
    /// </summary>
    ///
    public override void InitializeAcademy()
    {
        // GameObject.Find("TextGravity").GetComponent<Text>().text =
        // "Gravity: " + Physics.gravity.y.ToString("F2");
    }
}

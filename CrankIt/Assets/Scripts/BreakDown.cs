using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Paramètres")]
    public float minDelay = 15f;
    public float maxDelay = 30f;

    List<PipelineRenderer> verticalPipes = new List<PipelineRenderer>();
    PipelineRenderer brokenPipe = null;

    void Start()
    {
        ScheduleNextBreakdown();
    }

    public void RegisterPipe(PipelineRenderer pipe)
    {
        if (pipe.GetOrientation() == PipeOrientation.Vertical)
            verticalPipes.Add(pipe);
    }

    public void UnregisterPipe(PipelineRenderer pipe)
    {
        verticalPipes.Remove(pipe);
    }

    void ScheduleNextBreakdown()
    {
        float delay = Random.Range(minDelay, maxDelay);
        Invoke(nameof(BreakRandomPipe), delay);
    }

    void BreakRandomPipe()
    {
        // Filtre les pipes qui ne sont pas déjà cassés
        var candidates = verticalPipes.FindAll(p => p.GetState() != PipeState.Broken);
        if (candidates.Count == 0)
        {
            ScheduleNextBreakdown();
            return;
        }

        brokenPipe = candidates[Random.Range(0, candidates.Count)];
        brokenPipe.SetState(PipeState.Broken);

        // Notifie le mini-jeu
        RepairMinigame.Instance.StartRepair(brokenPipe, OnRepaired);
    }

    void OnRepaired()
    {
        brokenPipe = null;
        ScheduleNextBreakdown();
    }
}

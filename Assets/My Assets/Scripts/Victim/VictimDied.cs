using UnityEngine;
using System.Collections;

public class VictimDied : StateMachineBehaviour {

    AudioSource victimDiedAudio;
    Light victimDiedLight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        victimDiedAudio = animator.gameObject.GetComponent<AudioSource>();
        victimDiedAudio.Play();

        victimDiedLight = animator.gameObject.GetComponentInChildren<Light>();
        victimDiedLight.intensity = 10f;
        victimDiedLight.transform.position = new Vector3(victimDiedLight.transform.position.x, victimDiedLight.transform.position.y, -0.2f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        victimDiedAudio.Stop();
        GameStateManager.manlingsVictimized++;
        Destroy(animator.gameObject);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}

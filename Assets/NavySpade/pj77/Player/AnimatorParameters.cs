using System;
using UnityEngine;


namespace NavySpade.pj77.Player{
    internal class AnimatorParameters{
        private readonly Animator _animator;
        private int _run = Animator.StringToHash("Speed");
        private int _pickup = Animator.StringToHash("PickUp");

        public AnimatorParameters(Animator animator){
            _animator = animator;
        }

        public void SetRun(float state){
            if (Math.Abs(_animator.GetFloat(_run) - state) > 0.1f)
                _animator.SetFloat(_run, state);
        }

        public void SetPickup(float state){
            if (Math.Abs(_animator.GetFloat(_pickup) - state) > 0.1f)
                _animator.SetFloat(_pickup, state);
        }
    }
}
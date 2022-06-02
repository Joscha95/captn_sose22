using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JB
{
    public interface ICameraController {
        public float zoom
        {
            get; set;
        }


        public void Reset(bool soft=false);
    }

    public interface ITopicController{
        public void Reset();
        public void Activate();
        public void Deactivate();
    }
}


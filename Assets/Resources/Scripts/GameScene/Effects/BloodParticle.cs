using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class BloodParticle : ParticleScript
    {
        protected override void Disablethis()
        {
            ObjectPoolController.Instance.bloodPool.Push(this);
        }
    }
}

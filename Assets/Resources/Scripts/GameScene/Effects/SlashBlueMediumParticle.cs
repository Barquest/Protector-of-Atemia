using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class SlashBlueMediumParticle : ParticleScript
    {
        protected override void Disablethis()
        {
            ObjectPoolController.Instance.bloodPool.Push(this);
        }
    }
}

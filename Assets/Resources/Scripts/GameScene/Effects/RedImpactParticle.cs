using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class RedImpactParticle : ParticleScript
    {
		protected override void Disablethis()
		{
			ObjectPoolController.Instance.redImpactPool.Push(this);
		}
	}
}

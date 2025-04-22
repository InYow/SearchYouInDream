using UnityEngine;

namespace Projectile
{
    public class Projectile_Pencil : ProjectileBase
    {
        public float _prjectileSpeed;
        public float _projectileLastTime;

        private float spawnTime;

        public void OnEnable()
        {
            spawnTime = Time.time;
        }

        public override void EmmitProjectile(Vector3 direction)
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.simulated = true;
            _rb.velocity = direction * _prjectileSpeed;
        }

        public void Update()
        {
            if (Time.time - spawnTime > _projectileLastTime)
            {
                Destroy(this.gameObject);
            }
        }

        public override void Stop()
        {
            _rb.velocity = Vector2.zero;
            Destroy(this.gameObject);
        }
    }
}
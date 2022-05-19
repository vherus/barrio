using UnityEngine;
using Barrio.Enums;

namespace Barrio.Core
{
    public class TaggedCollider
    {
        private Collider2D collider;

        public TaggedCollider(Collider2D collider2D) {
            collider = collider2D;
        }

        public bool IsGround() {
            return collider.CompareTag(Tag.Ground.ToString());
        }

        public bool IsSolidBlock() {
            return collider.CompareTag(Tag.SolidBlock.ToString());
        }

        public bool IsQuestionBlock() {
            return collider.CompareTag(Tag.QuestionBlock.ToString());
        }

        public bool IsBottom() {
            return collider.CompareTag(Tag.Bottom.ToString());
        }
    }
}
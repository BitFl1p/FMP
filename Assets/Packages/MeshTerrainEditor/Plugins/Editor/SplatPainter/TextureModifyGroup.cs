using System.Collections.Generic;
using UnityEngine;

namespace MTE
{
    internal struct TextureModifyGroup
    {
        public GameObject gameObject;

        public int splatIndex;
        public int splatTotal;
        public Texture2D controlTexture0;
        public Texture2D controlTexture1;

        public Rect texelRect;
        public Vector2 minUV;
        public Vector2 maxUV;


        public TextureModifyGroup(GameObject gameObject, int splatIndex, int splatTotal, Texture2D controlTexture0, Texture2D controlTexture1, Rect texelRect, Vector2 minUV, Vector2 maxUV)
        {
            this.gameObject = gameObject;

            this.splatIndex = splatIndex;
            this.splatTotal = splatTotal;
            this.controlTexture0 = controlTexture0;
            this.controlTexture1 = controlTexture1;

            this.texelRect = texelRect;
            this.minUV = minUV;
            this.maxUV = maxUV;
        }

    }
}
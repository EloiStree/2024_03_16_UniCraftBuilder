using UnityEngine;

public class ScratchHolderMono_Texture2D : AbstractScratchMono_Texture2DImageHolder
{
    public Texture2D m_image;
    public override Texture2D GetAsTexture2D()
    {
        return m_image;
    }

    public override void SetWithTexture2D(Texture2D texture)
    {
        m_image = texture;
    }
}

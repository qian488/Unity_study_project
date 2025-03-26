using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.AGUI
{
    public class CustomGUILabel : CustomGUIControl
    {
        protected override void StyleOffDraw()
        {
            GUI.Label(guiPos.Pos, content);
        }
        protected override void StyleOnDraw()
        {
            GUI.Label(guiPos.Pos, content, style);
        }
    }
}
using UnityEngine;

namespace Runningboy.UI
{
    public class WorldMapPanel : UIView
    {
        public override void Show()
        {
            base.Show();

            Time.timeScale = 0;
        }

        public override void Hide()
        {
            base.Hide();

            Time.timeScale = 1f;
        }
    }
}
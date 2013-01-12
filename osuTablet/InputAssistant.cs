using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace osuTablet
{
    /// <summary>
    /// IA(InputAssistant) 클래스는 입력을 도와주는 역할을 담당합니다.
    /// 마우스 포인터를 움직이거나 클릭, 키보드 입력 부분을 도와줍니다.
    /// </summary>
    class InputAssistant
    {
        /// <summary>
        /// 바운딩 박스를 저장하는 변수입니다.
        /// </summary>
        private static Rectangle boundingBox = new Rectangle(new Point(0, 0), new Size(0x3939, 0x3939));

        /// <summary>
        /// 바운딩 박스를 설정합니다.
        /// </summary>
        /// <param name="location">박스의 시작점을 가리킵니다. 단 시작점은 사각형의 왼쪽 위의 점이어야 합니다.</param>
        /// <param name="size">박스의 크기를 가리킵니다.</param>
        public static void SetBoundingBox(Point location, Size size)
        {
            InputAssistant.boundingBox = new Rectangle(location, size);
        }

        /// <summary>
        /// 마우스 포인터를 이동시킵니다.
        /// 지정된 바운딩 박스를 벗어나는 좌표가 들어오면 좌표를 바운딩 박스 안으로 수정합니다.
        /// </summary>
        /// <param name="absolute">이동할 좌표를 가지고 있는 구조체입니다. 단 좌표는 절대 좌표이여야 합니다.</param>
        public static void MoveCursor(Point absolute)
        {
            // absolute.X = min(max(absolute.X, boundingBox.X), boundingBox.Right);
            if (boundingBox.X > absolute.X)
            {
                absolute.X = boundingBox.X;
            }
            else if (boundingBox.Right < absolute.X)
            {
                absolute.X = boundingBox.Right;
            }

            // absolute.Y = min(max(absolute.Y, boundingBox.Y), boundingBox.Bottom);
            if (boundingBox.Y > absolute.Y)
            {
                absolute.Y = boundingBox.Y;
            }
            else if (boundingBox.Bottom < absolute.Y)
            {
                absolute.Y = boundingBox.Bottom;
            }

            InputAssistant.SetCursorPos(absolute.X, absolute.Y);
        }

        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint flags, uint dx, uint dy, uint data, ref uint extrainfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte vk, byte scan, uint flag, ref uint extrainfo);

    }
}

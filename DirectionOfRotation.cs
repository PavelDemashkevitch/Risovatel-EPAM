using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Renderer
    {
        static float x, y;
        static Graphics graphic;

        public static void Initializer(Graphics newGraphic)
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void Draw(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double angleOfRotatiton, Graphics graphics)
        {
            Renderer.Initializer(graphics);

            var size = Math.Min(width, height);

            var diagonalLength = Math.Sqrt(2) * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Renderer.SetPosition(x0, y0);

            //Рисуем 1-ую сторону
            DrawSide(size, 0, angleOfRotatiton);

            //Рисуем 2-ую сторону
            DrawSide(size, -Math.PI / 2, angleOfRotatiton);

            //Рисуем 3-ю сторону
            DrawSide(size, Math.PI, angleOfRotatiton);

            //Рисуем 4-ую сторону
            DrawSide(size, Math.PI / 2, angleOfRotatiton);
        }

        private static void DrawSide(int size, double angle, double angleOfRotation)
        {
            double speedRotation = 3;
            angle += (int)DirectionOfRotation.Left * (speedRotation * angleOfRotation);

            Renderer.Draw(Pens.Yellow, size * 0.375f, angle);
            Renderer.Draw(Pens.Yellow, size * 0.04f * Math.Sqrt(2), angle + Math.PI / 4);
            Renderer.Draw(Pens.Yellow, size * 0.375f, angle + Math.PI);
            Renderer.Draw(Pens.Yellow, size * 0.375f - size * 0.04f, angle + Math.PI / 2);

            Renderer.Change(size * 0.04f, angle - Math.PI);
            Renderer.Change(size * 0.04f * Math.Sqrt(2), angle + 3 * Math.PI / 4);
        }

        enum DirectionOfRotation
        {
            Left = -1,
            Right = 1
        }
    }
}
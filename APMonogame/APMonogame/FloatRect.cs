using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMonogame
{
    public class FloatRect
    {
        float top, bottom, left, right;
        public float Top
        {
            get { return top; }
            set { top = value; }
            

        }
         public float Bottom
        {
            get { return bottom; }
            set { bottom = value; }

        }
        
        public float Left
        {
            get { return left; }
            set { left = value; }

        }
        public float Right
        {
            get { return right; }
            set { right = value; }

        }

        public FloatRect(float x, float y, float width, float height)

        {
            left = x;
            right = x + width;
            top = y;
            bottom = y + height;
        }

        public bool Intersects(FloatRect f)
        {
            if (right <= f.left || left >= f.right || top >= f.bottom || bottom <= f.top)
                return false;
            return true;
        }
    }
}

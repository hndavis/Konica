using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;

namespace GameServer.Game
{
    public class Geometry
    {
        //adapted from https://martin-thoma.com/how-to-check-if-two-line-segments-intersect/

        public static  double EPSILON = 0.000001;

        /**
         * Calculate the cross product of two points.
         * @param a start point
         * @param b end point
         * @return the value of the cross product
         */
        public static double crossProduct(Point a, Point b)
        {
            return a.x * b.y - b.x * a.y;
        }

        /**
         * Check if bounding boxes do intersect. If one bounding box
         * touches the other, they do intersect.
         * @param a start bounding box
         * @param b end bounding box
         * @return <code>true</code> if they intersect,
         *         <code>false</code> otherwise.
         */
        public static bool doBoundingBoxesIntersect(Point[] a, Point[] b)
        {
            return a[0].x <= b[1].x && a[1].x >= b[0].x && a[0].y <= b[1].y
                    && a[1].y >= b[0].y;
        }

        /**
         * Checks if a Point is on a line
         * @param a line (interpreted as line, although given as line
         *                segment)
         * @param b point
         * @return <code>true</code> if point is on line, otherwise
         *         <code>false</code>
         */
        public static bool isPointOnLine(Line a, Point b)
        {
            // Move the image, so that a.start is on (0|0)
            Line aTmp = new Line(new Point(0, 0), new Point(
                    a.end.x - a.start.x, a.end.y - a.start.y));
            Point bTmp = new Point(b.x - a.start.x, b.y - a.start.y);
            double r = crossProduct(aTmp.end, bTmp);
            return Math.Abs(r) < EPSILON;
        }

        /**
         * Checks if a point is right of a line. If the point is on the
         * line, it is not right of the line.
         * @param a line segment interpreted as a line
         * @param b the point
         * @return <code>true</code> if the point is right of the line,
         *         <code>false</code> otherwise
         */
        public static bool isPointRightOfLine(Line a, Point b)
        {
            // Move the image, so that a.start is on (0|0)
            Line aTmp = new Line(new Point(0, 0), new Point(
                    a.end.x - a.start.x, a.end.y - a.start.y));
            Point bTmp = new Point(b.x - a.start.x, b.y - a.start.y);
            return crossProduct(aTmp.end, bTmp) < 0;
        }

        /**
         * Check if line segment start touches or crosses the line that is
         * defined by line segment end.
         *
         * @param start line segment interpreted as line
         * @param end line segment
         * @return <code>true</code> if line segment start touches or
         *                           crosses line end,
         *         <code>false</code> otherwise.
         */
        public static bool lineSegmentTouchesOrCrossesLine(Line a,
                Line b)
        {
            return isPointOnLine(a, b.start)
                    || isPointOnLine(a, b.end)
                    || (isPointRightOfLine(a, b.start) ^ isPointRightOfLine(a,
                            b.end));
        }

        /**
         * Check if line segments intersect
         * @param a start line segment
         * @param b end line segment
         * @return <code>true</code> if lines do intersect,
         *         <code>false</code> otherwise
         */
        public static bool doLinesIntersect(Line a, Line b)
        {
            Point[] box1 = a.getBoundingBox();
            Point[] box2 = b.getBoundingBox();
            return doBoundingBoxesIntersect(box1, box2)
                    && lineSegmentTouchesOrCrossesLine(a, b)
                    && lineSegmentTouchesOrCrossesLine(b, a);
        }
    }
}

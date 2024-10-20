using UnityEngine;

public class BezierMovement 
{
    Vector2 start;
    Vector2 end;
    float speed;
    Vector2 bezierPosition;

    Vector2 controlPoint1 = Vector2.zero;
    Vector2 controlPoint2 = Vector2.zero;
    float t = 0f;
    float maxValueT = 1f;

    public bool inTheEnd { get; private set; }

    public void Reset(Vector2 startPoint, Vector2 endPoint, float _speed)
    {
        inTheEnd = false;
        t = 0f;
        start = startPoint;
        end = endPoint;
        speed = _speed;
        GenerateRandomControlPoints();
    }

    private void GenerateRandomControlPoints()
    {
        Vector2 direction = (start - end).normalized;
        float distance = Vector2.Distance(start, end);

        controlPoint1 = start + direction * (distance / 3) + Random.insideUnitCircle * (distance / 2);

        controlPoint2 = end - direction * (distance / 3) + Random.insideUnitCircle * (distance / 2);
    }

    public void MoveFollowCubicBezierPointFixedUpdate(Transform transform, bool needRotationAndFlip)
    {
        inTheEnd = (t >= maxValueT) ? true : false;

        if (inTheEnd)
        {
            return;
        }

        t += Time.fixedDeltaTime * speed;

        bezierPosition = CalculateCubicBezierPoint();
        
        if (needRotationAndFlip)
        {
            FlipAndRotateFollowTarget(transform);
        }

        transform.position = new Vector3(bezierPosition.x, bezierPosition.y, transform.position.z);
    }

    public void MoveFollowCubicBezierPointFixedUpdate(Transform transform, Vector2 newEnd , bool needRotationAndFlip)
    {
        inTheEnd = (t >= maxValueT) ? true : false;

        if (inTheEnd)
        {
            return;
        }

        t += Time.fixedDeltaTime * speed;

        bezierPosition = CalculateCubicBezierPoint(newEnd);

        if (needRotationAndFlip)
        {
            FlipAndRotateFollowTarget(transform);
        }

        transform.position = new Vector3(bezierPosition.x, bezierPosition.y, transform.position.z);
    }

    private Vector2 CalculateCubicBezierPoint()
    {
        Vector2 point = Mathf.Pow((1 - t), 3) * start; // (1 - t)^3 * P0
        point += 3 * Mathf.Pow((1 - t), 2) * t * controlPoint1; // 3 * (1 - t)^2 * t * P1
        point += 3 * (1 - t) * t * t * controlPoint2; // 3 * (1 - t) * t^2 * P2
        point += t * t * t * end;        // t^3 * P3

        return point;
    }

    private Vector2 CalculateCubicBezierPoint(Vector2 _end)
    {
        Vector2 point = Mathf.Pow((1 - t), 3) * start; // (1 - t)^3 * P0
        point += 3 * Mathf.Pow((1 - t), 2) * t * controlPoint1; // 3 * (1 - t)^2 * t * P1
        point += 3 * (1 - t) * t * t * controlPoint2; // 3 * (1 - t) * t^2 * P2
        point += t * t * t * _end;        // t^3 * P3

        return point;
    }


    private void FlipAndRotateFollowTarget(Transform transform)
    {
        Vector2 a = transform.position;
        Vector2 b = end;
        Vector2 c = new Vector2(b.x, a.y);
        float _angle = Vector2.Angle(b - a, c - a);

        if (b.y < a.y)
        {
            _angle = -_angle;
        }

        if (end.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, _angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
    }
}

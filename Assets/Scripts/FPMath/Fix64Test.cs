using UnityEngine;
using System.Collections;
using FixMath;

public class Fix64Test : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {
        AddTest();
        //SubtractTest();
        //MultiplyTest();
        //DivisionTest();
        //SqrtTest();
        //Atan2Test();
        FixVec2Test();
    }

    private void AddTest()
    {
        Debug.Log("------------AddTest-----------");
        Fix64 res1 = (Fix64)2.222 + (Fix64)1.111;
        Debug.Log(res1);
        Debug.Log(((float)res1).ToString("f3"));
        Debug.Log("\n");
    }

    private void SubtractTest()
    {
        Debug.Log("------------SubtractTest-----------");
        Fix64 res1 = (Fix64)2.2 - (Fix64)1.1;
        Debug.Log((float)res1);
        Debug.Log("\n");
    }

    private void MultiplyTest()
    {
        Debug.Log("------------MultiplyTest-----------");
        Fix64 res1 = (Fix64)1.1 * (Fix64)1.1;
        Debug.Log((float)res1);
        Debug.Log("\n");
    }

    private void DivisionTest()
    {
        Debug.Log("------------DivisionTest-----------");
        Fix64 res1 = (Fix64)1.2 / (Fix64)5;
        Debug.Log((float)res1);
        Debug.Log("\n");
    }

    private void SqrtTest()
    {
        Debug.Log("------------SqrtTest-----------");
        Fix64 res1 = Fix64.Sqrt((Fix64)2);
        Debug.Log((float)res1);
        Debug.Log("\n");
    }

    private void Atan2Test()
    {
        Debug.Log("------------Atan2Test-----------");
        // 60度
        Fix64 res1 = Fix64.Atan2((Fix64)Mathf.Sqrt(3), Fix64.One);
        Debug.Log((float)(res1 * Fix64.Rad2Deg));
        Debug.Log("\n");
    }

    private void FixVec2Test()
    {
        Debug.Log("------------FixVec2Test-----------");
        FixVec2 res1 = new FixVec2(2.222f, 4.444f);
        Debug.Log(res1);
        res1 = res1.Normalize();
        Debug.Log(res1);
        Debug.Log(res1.ToVector2().ToString("f3"));

        Vector2 res2 = new Vector2(2.222f, 4.444f);
        Debug.Log(res2.ToString("f3"));
        res2.Normalize();
        Debug.Log(res2.ToString("f3"));
        Debug.Log("\n");
    }
}

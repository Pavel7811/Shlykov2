using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Расчёт результатов тестирования.
/// </summary>
public static class EvaluatingSystem
{
    public static void CalculateResult(UserResult userResult)
    {
        if (userResult.CountQuestions == userResult.Answers.Count)
        {
            int countAnswerWithOneAtt = userResult.Answers.Count(c => c.CountAttemps == 0);
            int res = Mathf.RoundToInt((countAnswerWithOneAtt *1f)/(userResult.CountQuestions * 1f) * 100f);
            userResult.ResultValue = res.ToString() + "%";
        }
        else
        {
            userResult.ResultValue = "-";
        }
    }
}

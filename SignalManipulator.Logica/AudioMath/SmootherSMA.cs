﻿using SignalManipulator.Logic.AudioMath;
using System.Collections.Generic;
using System;

public class SmootherSMA : Smoother
{
    private readonly Queue<double[]> history = new Queue<double[]>();
    private int maxHistory;

    public SmootherSMA(int historyLength)
    {
        if (historyLength < 1)
            throw new ArgumentOutOfRangeException(nameof(historyLength), "History length must be ≥ 1");

        maxHistory = historyLength;
    }

    public void SetHistoryLength(int historyLength) => maxHistory = historyLength;

    public override double[] Smooth(double[] input)
    {
        history.Enqueue((double[])input.Clone());
        while (history.Count > maxHistory)
            history.Dequeue();

        int length = input.Length;
        double[] result = new double[length];

        foreach (var arr in history)
            for (int i = 0; i < length; i++)
                result[i] += arr[i];

        for (int i = 0; i < length; i++)
            result[i] /= history.Count;

        return result;
    }
}
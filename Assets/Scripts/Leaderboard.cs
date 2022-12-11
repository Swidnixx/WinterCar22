using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct Car
{
    public string name;
    public int position;

    public Car(string name, int position)
    {
        this.name = name;
        this.position = position;
    }
}

public class Leaderboard
{
    static Dictionary<int, Car> board = new Dictionary<int, Car>();

    static int carsRegistered = -1;

    public static void Reset()
    {
        board.Clear();
        carsRegistered = -1;
    }

    public static int RegisterCar(string name)
    {
        carsRegistered++;
        board.Add(carsRegistered, new Car(name, 0));
        return carsRegistered;
    }

    internal static void SetPosition(int carId, int lap, int checkpoint)
    {
        int position = lap * 1000 + checkpoint * 1;
        board[carId] = new Car(board[carId].name, position);
    }

    internal static List<string> GetPlaces()
    {
        return board.OrderByDescending(entry => entry.Value.position).Select(entry => entry.Value.name).ToList();
    }
}

﻿using Event.Events;
using Plugins.Lowscope.ComponentSaveSystem.Interfaces;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;
using World;

namespace Weather
{
    public class WeatherDirtTileSetter : MonoBehaviour, ISaveable
    {
        [SerializeField]
        private GridManager gridManager;

        [SerializeField]
        private WeatherEvent weatherEvent;

        [SerializeField]
        private TimeEvent timeEvent;

        private DateTime lastDateTime;
        private bool isNewDay = false;

        private TileBase[] getAllDirtTiles;

        private void OnEnable()
        {
            weatherEvent.AddListener(OnWeatherChanged);
            timeEvent.AddListener(OnDayPassed);
        }

        private void OnDisable()
        {
            weatherEvent.RemoveListener(OnWeatherChanged);
            timeEvent.RemoveListener(OnDayPassed);
        }

        private void OnDayPassed(DateTime dateTime)
        {
            // Check if this day is actually new
            if (lastDateTime.DayOfYear != dateTime.DayOfYear)
            {
                lastDateTime = dateTime;
                isNewDay = true;
            }
        }

        private void OnWeatherChanged(EWeather eWeather)
        {
            if (!isNewDay)
                return;

            if (eWeather == EWeather.Rainy)
            {
                Tilemap dirtHoleTileMap = gridManager?.DirtHoleTileMap;

                if (dirtHoleTileMap == null)
                    return;

                // Set all tiles wet
                foreach (var pos in dirtHoleTileMap.cellBounds.allPositionsWithin)
                {
                    if (dirtHoleTileMap.HasTile(pos))
                    {
                        gridManager.SetWateredDirtTile(pos);
                    }
                }
            }
            else
            {
                gridManager.WateredDirtTileMap.ClearAllTiles();
                gridManager.ClearTileMapData(gridManager.WateredDirtTileMap);
            }
        }

        [System.Serializable]
        public class SaveData
        {
            public DateTime lastDateTime;
        }

        public string OnSave()
        {
            return lastDateTime.ToString(CultureInfo.InvariantCulture);
        }

        public void OnLoad(string data)
        {
            if (DateTime.TryParse(data, out var date))
                lastDateTime = date;
        }

        public bool OnSaveCondition()
        {
            return true;
        }
    }
}

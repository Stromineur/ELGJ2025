using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LTX.ChanneledProperties;
using UnityEngine;

namespace Script.Core
{
    public static class GameController
    {
        public static GameMetrics GameMetrics => _gameMetrics;

        private static GameMetrics _gameMetrics;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeOnLoad()
        {
            Application.targetFrameRate = 60;

            _gameMetrics = Resources.Load<GameMetrics>("GameMetrics");
        }

        public static void Quit()
        {
            Application.Quit();
        }
    }
}
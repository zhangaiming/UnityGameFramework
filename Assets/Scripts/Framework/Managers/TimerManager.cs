using System;
using System.Collections.Generic;
using Framework.Utils;
using UnityEngine;

namespace Framework.Managers
{
    public class TimerManager : UnitySingleton<TimerManager>
    {
        public delegate void OnTickHandler(int timerId, float curTicks, object param);

        public delegate void OnCompleteHandler(int timerId, object param);

        class Timer
        {
            public int TimerId;
            // 计时器是否受时间缩放的影响
            public bool ShouldScaled = true;
            // 第一次之前的延迟
            public float Delay;
            // 两次重复之间的时间间隔
            public float Interval;
            // 总共的重复次数
            public int    Times;
            public int    CurTimes = 0;
            public float  Ticks    = 0f;
            public object Param    = null;

            public OnTickHandler     OnTick     = null;
            public OnCompleteHandler OnComplete = null;
        }
        
        // 计时器字典
        Dictionary<int, Timer> timers = new();
        
        // 自增id编号
        int autoIncId = 1;

        void Update()
        {
            List<int> removeCache = new();
            
            float deltaTime = Time.deltaTime;
            float unscaledDeltaTime = Time.unscaledDeltaTime;
            foreach ((int timerId, Timer timer) in timers)
            {
                timer.Ticks += timer.ShouldScaled ? deltaTime : unscaledDeltaTime;
                timer.OnTick?.Invoke(timerId, timer.Ticks, timer.Param);

                if (timer.CurTimes == 0 && timer.Ticks >= timer.Delay || timer.CurTimes > 0 && timer.Ticks >= timer.Interval)
                {
                    timer.OnComplete?.Invoke(timerId, timer.Param);
                    timer.Ticks = 0;
                    timer.CurTimes += 1;
                    if (timer.Times > 0 && timer.CurTimes >= timer.Times)
                    {
                        removeCache.Add(timerId);
                    }
                }
            }
            
            foreach (int completeTimerId in removeCache)
            {
                timers.Remove(completeTimerId);
            }
        }

        /// <summary>
        /// 启动一个定时器，并获取对应的计时器id
        /// </summary>
        /// <param name="delay">第一次开始任务之前的延迟，若该值小于等于0，则在下一帧就会执行目标任务</param>
        /// <param name="times">总重复次数，小于等于0代表无限次重复</param>
        /// <param name="interval">两次重复执行之间的时间间隔，若小于等于0，则在下一帧立即重复执行</param>
        /// <param name="onTickHandler"></param>
        /// <param name="onCompleteHandler"></param>
        /// <returns></returns>
        public int Schedule(
            float delay, 
            int times, 
            float interval,
            OnTickHandler onTickHandler = null, 
            OnCompleteHandler onCompleteHandler = null)
        {
            return Schedule(delay, times, interval, null, true, onTickHandler, onCompleteHandler);
        }
        
        /// <summary>
        /// 启动一个定时器，并获取对应的计时器id
        /// </summary>
        /// <param name="delay">第一次开始任务之前的延迟，若该值小于等于0，则在下一帧就会执行目标任务</param>
        /// <param name="times">总重复次数，小于等于0代表无限次重复</param>
        /// <param name="interval">两次重复执行之间的时间间隔，若小于等于0，则在下一帧立即重复执行</param>
        /// <param name="shouldScaled"></param>
        /// <param name="onTickHandler"></param>
        /// <param name="onCompleteHandler"></param>
        /// <returns></returns>
        public int Schedule(
            float delay, 
            int times, 
            float interval,
            bool shouldScaled = true,
            OnTickHandler onTickHandler = null, 
            OnCompleteHandler onCompleteHandler = null)
        {
            return Schedule(delay, times, interval, null, shouldScaled, onTickHandler, onCompleteHandler);
        }

        /// <summary>
        /// 启动一个定时器，并获取对应的计时器id
        /// </summary>
        /// <param name="delay">第一次开始任务之前的延迟，若该值小于等于0，则在下一帧就会执行目标任务</param>
        /// <param name="times">总重复次数，小于等于0代表无限次重复</param>
        /// <param name="interval">两次重复执行之间的时间间隔，若小于等于0，则在下一帧立即重复执行</param>
        /// <param name="shouldScaled"></param>
        /// <param name="onTickHandler"></param>
        /// <param name="onCompleteHandler"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Schedule(
            float delay, 
            int times, 
            float interval, 
            object param,
            bool shouldScaled = true,
            OnTickHandler onTickHandler = null, 
            OnCompleteHandler onCompleteHandler = null)
        {
            // 初始化计时器
            Timer timer = new();
            int timerId = autoIncId;
            autoIncId++;
            timer.TimerId = timerId;
            timer.ShouldScaled = shouldScaled;
            timer.Delay = delay;
            timer.Interval = interval;
            timer.Param = param;
            timer.OnTick = onTickHandler;
            timer.OnComplete = onCompleteHandler;
            // end
            
            // 将计时器加入字典中
            timers.Add(timerId, timer);
            // end
            
            return timerId;
        }

        public int ScheduleOnce(
            float delay,
            OnTickHandler onTickHandler = null, 
            OnCompleteHandler onCompleteHandler = null)
        {
            return Schedule(delay, 1, 0f, null, true, onTickHandler, onCompleteHandler);
        }
        
        /// <summary>
        /// 启动一个只重复一次的定时器，并获取对应的计时器id
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="shouldScaled"></param>
        /// <param name="onTickHandler"></param>
        /// <param name="onCompleteHandler"></param>
        /// <returns></returns>
        public int ScheduleOnce(
            float delay, 
            bool shouldScaled = true,
            OnTickHandler onTickHandler = null, 
            OnCompleteHandler onCompleteHandler = null)
        {
            return Schedule(delay, 1, 0f, null, shouldScaled, onTickHandler, onCompleteHandler);
        }

        /// <summary>
        /// 启动一个只重复一次的定时器，并获取对应的计时器id
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="param"></param>
        /// <param name="onTickHandler"></param>
        /// <param name="onCompleteHandler"></param>
        /// <returns></returns>
        public int ScheduleOnce(
            float delay, 
            object param,
            OnTickHandler onTickHandler = null, 
            OnCompleteHandler onCompleteHandler = null)
        {
            return Schedule(delay, 1, 0f, param, true, onTickHandler, onCompleteHandler);
        }
        
        /// <summary>
        /// 启动一个只重复一次的定时器，并获取对应的计时器id
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="param"></param>
        /// <param name="shouldScaled"></param>
        /// <param name="onTickHandler"></param>
        /// <param name="onCompleteHandler"></param>
        /// <returns></returns>
        public int ScheduleOnce(
            float delay, 
            object param,
            bool shouldScaled = true,
            OnTickHandler onTickHandler = null, 
            OnCompleteHandler onCompleteHandler = null)
        {
            return Schedule(delay, 1, 0f, param, shouldScaled, onTickHandler, onCompleteHandler);
        }
        
        /// <summary>
        /// 取消一个计时器
        /// </summary>
        /// <param name="timerId"></param>
        public void Cancel(int timerId)
        {
            if (!timers.ContainsKey(timerId))
            {
                throw new KeyNotFoundException($"取消计时器时出错：不存在id为{timerId}的计时器");
            }

            timers.Remove(timerId);
        }
    }
}
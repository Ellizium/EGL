using EGL.GodotBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGL.Extension.Utility
{
    public class Timer
    {
        private Action Callback;
        private INode Owner;

        public float NeedTime { get; set; }
        public float CurrentTime { get; set; } = 0;
        public bool IsRepeat { get; set; }
        public bool IsPhysics { get; set; }
        public int CountRepeat { get; set; } = -1;

        public bool IsActive { get; private set; } = false;

        public Timer(INode owner, float needTime, bool isRepeat, bool isPhysics, Action callBack)
        {
            Owner = owner;
            NeedTime = needTime;
            IsRepeat = isRepeat;
            Callback = callBack;
            IsPhysics = isPhysics;
        }
        public Timer(INode owner, float needTime, bool isRepeat, bool isPhysics, int countRepeat, Action callBack)
        {
            Owner = owner;
            NeedTime = needTime;
            IsRepeat = isRepeat;
            CountRepeat = countRepeat - 1;
            Callback = callBack;
            IsPhysics = isPhysics;
        }
        public void Start()
        {
            if (IsPhysics)
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        Owner.PhysicsProcess += UpdateCoutRepeat;
                    }
                    else
                    {
                        Owner.PhysicsProcess += UpdateRepeat;
                    }
                }
                else
                {
                    Owner.PhysicsProcess += Update;
                }
            }
            else
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        Owner.RenderProcess += UpdateCoutRepeat;
                    }
                    else
                    {
                        Owner.RenderProcess += UpdateRepeat;
                    }
                }
                else
                {
                    Owner.RenderProcess += Update;
                }
            }

            IsActive = true;
        }
        public void Stop(bool resetTime)
        {
            if (IsPhysics)
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Owner.PhysicsProcess -= UpdateCoutRepeat;
                    }
                    else
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Owner.PhysicsProcess -= UpdateRepeat;
                    }
                }
                else
                {
                    if (resetTime)
                        CurrentTime = 0;
                    Owner.PhysicsProcess -= Update;
                }
            }
            else
            {
                if (IsRepeat)
                {
                    if (CountRepeat != -1)
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Owner.RenderProcess -= UpdateCoutRepeat;
                    }
                    else
                    {
                        if (resetTime)
                            CurrentTime = 0;
                        Owner.RenderProcess -= UpdateRepeat;
                    }
                }
                else
                {
                    if (resetTime)
                        CurrentTime = 0;
                    Owner.RenderProcess -= Update;
                }
            }

            IsActive = false;
        }
        private void Update(float delta)
        {
            if (CurrentTime >= NeedTime)
            {
                Stop(true);
                Callback();
                return;
            }

            CurrentTime += delta;
        }
        private void UpdateRepeat(float delta)
        {
            if (CurrentTime >= NeedTime)
            {
                CurrentTime = 0;
                Callback();
            }

            CurrentTime += delta;
        }
        private void UpdateCoutRepeat(float delta)
        {
            if (CurrentTime >= NeedTime)
            {
                if (CountRepeat > 0)
                {
                    CountRepeat--;
                    CurrentTime = 0;
                    Callback();
                }
                else
                {
                    Callback();
                    Stop(true);
                    return;
                }
            }

            CurrentTime += delta;
        }
    }
}

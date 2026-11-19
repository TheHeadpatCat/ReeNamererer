using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ReeNamererer
{
    public class MainWindow : Window, IDisposable
    {
        Plugin Plugin;
        private static float divideWidth = 5;
        private List<Entry> removeQueue = new List<Entry>();

        public MainWindow(string name, ImGuiWindowFlags flags = ImGuiWindowFlags.None, bool forceMainWindow = false) : base(name, flags, forceMainWindow)
        {
        }

        public MainWindow(string name, Plugin plugin) : base(name)
        {
            Plugin = plugin;
            SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(870, 400),
                MaximumSize = new Vector2(1100, 1200)
            };
        }

        public override void Draw()
        {
            float windowWidth = ImGui.GetWindowWidth();
            ImGui.Text("Use Glamourer to Find IDs of NPCs in your Area");
            ImGui.Text("You have to press ENTER when finishing any edit. Otherwise it won't save.");

            ImGui.BeginGroup();
            ImGui.Text("NPC Id List. Seperated by ,     ||    Real NPC Name    ||    Nickname for NPC    ||    Enable Title    ||    Title for NPC");

            int i = 0;
            foreach (var entry in Plugin.Configuration.Entries)
            {
                ImGui.BeginGroup();

                ImGui.SetNextItemWidth(windowWidth / divideWidth);
                string idString = entry.GetIDString();
                if(ImGui.InputTextWithHint($"##entryIDs{i}","Enter IDs seperated with \",\"", ref idString, 512, ImGuiInputTextFlags.EnterReturnsTrue))
                {
                    if (!Plugin.Configuration.Entries[i].SetIDsFromString(idString))
                    {
                        throw new Exception("Failed to bind IDs from string. Misformated?");
                    }
                    else
                        Plugin.Configuration.Save();
                }

                ImGui.SetNextItemWidth(windowWidth / divideWidth);
                ImGui.SameLine();
                string realNameString = entry.RealName;
                if (ImGui.InputTextWithHint($"##entryRealName{i}", "Real Name", ref realNameString, 512, ImGuiInputTextFlags.EnterReturnsTrue))
                {
                    Plugin.Configuration.Entries[i].RealName = realNameString;
                    Plugin.Configuration.Save();
                }

                ImGui.SetNextItemWidth(windowWidth / divideWidth);
                ImGui.SameLine();
                string nickNameString = entry.NickName;
                if (ImGui.InputTextWithHint($"##entryNickName{i}", "Nick Name", ref nickNameString, 512, ImGuiInputTextFlags.EnterReturnsTrue))
                {
                    Plugin.Configuration.Entries[i].NickName = nickNameString;
                    Plugin.Configuration.Save();
                }

                ImGui.SetNextItemWidth(windowWidth / divideWidth);
                ImGui.SameLine();
                bool isTitleEnabled = entry.IsTitleEnabled;
                if (ImGui.Checkbox($"Show Title?##entryTitleEnabled{i}", ref isTitleEnabled))
                {
                    Plugin.Configuration.Entries[i].IsTitleEnabled = isTitleEnabled;
                    Plugin.Configuration.Save();
                }

                ImGui.SetNextItemWidth(windowWidth / divideWidth);
                ImGui.SameLine();
                string titleString = entry.Title;
                if (ImGui.InputTextWithHint($"##entryTitle{i}", "Title", ref titleString, 512, ImGuiInputTextFlags.EnterReturnsTrue))
                {
                    Plugin.Configuration.Entries[i].Title = titleString;
                    Plugin.Configuration.Save();
                }

                ImGui.SetNextItemWidth(45);
                ImGui.SameLine();
                if (ImGui.Button($"Delete##entryRemove{i}"))
                {
                    removeQueue.Add(entry);
                }

                ImGui.EndGroup();

                i++;
            }

            ImGui.EndGroup();
            
            if(ImGui.Button("Add new Entry"))
            {
                Plugin.Configuration.Entries.Add(new Entry());
                Plugin.Configuration.Save();
            }

            if(removeQueue.Count > 0)
            {
                foreach (var item in removeQueue)
                {
                    Plugin.Configuration.Entries.Remove(item);
                }
                Plugin.Configuration.Save();
                removeQueue.Clear();
            }
        }

        private void DrawEntry(Entry entry)
        {
            if(ImGui.ArrowButton($"##{entry.RealName}", ImGuiDir.Right))
            {

            }
        }

        public void Dispose()
        {
            if (this.IsOpen) this.Toggle();
        }
    }
}

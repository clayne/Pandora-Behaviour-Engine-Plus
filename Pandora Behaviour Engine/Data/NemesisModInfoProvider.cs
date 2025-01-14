﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pandora.Core;
using Pandora.API.Patch;
namespace Pandora.MVVM.Data;

public class NemesisModInfoProvider : IModInfoProvider
{
    public async Task<List<IModInfo>> GetInstalledMods(string folderPath) => await Task.Run(() => GetInstalledMods(new DirectoryInfo(folderPath)));

    private static List<IModInfo> GetInstalledMods(DirectoryInfo folder)
    {
		List<IModInfo> infoList = new List<IModInfo>();
        if (!folder.Exists) { return infoList;  }

        var modFolders = folder.GetDirectories();

        foreach( var modFolder in modFolders ) 
        {
            var infoFile = new FileInfo(Path.Join(modFolder.FullName, "info.ini"));
            if (!infoFile.Exists || infoFile.Directory is null) 
            { 
                continue; 
            }
            infoList.Add(NemesisModInfo.ParseMetadata(infoFile));
        }

        return infoList;
	}
}

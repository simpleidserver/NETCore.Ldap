// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using NETCore.Ldap.Exceptions;
using NETCore.Ldap.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NETCore.Ldap.Parser
{
    public class LDIFParser
    {
        public ICollection<IChangeRecord> Parse(string filePath)
        {
            var matchVersion = new Regex(@"version:\d");
            var matchDN = new Regex(@"dn:(\,?(\w|\d|\-)*=((\w|\d|\.)*))*");
            var result = new List<IChangeRecord>();
            using (var streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Replace(" ", "");
                    if (line.StartsWith("#") || line == string.Empty)
                    {
                        continue;
                    }

                    var splittedLine = line.Split(':');
                    if (matchVersion.IsMatch(line))
                    {
                        int version;
                        if (!int.TryParse(splittedLine.Last(), out version))
                        {
                            throw new LDIFParserException(Global.InvalidVersion);
                        }

                        if (version != 1)
                        {
                            throw new LDIFParserException(Global.InvalidVersion);
                        }
                    }

                    if (matchDN.IsMatch(line))
                    {
                        var distinguishedName = splittedLine.Last();
                        var changeAdd = new ChangeAdd(distinguishedName);
                        while((line = streamReader.ReadLine()) != null)
                        {
                            line = line.Replace(" ", "");
                            if (line.StartsWith("#"))
                            {
                                continue;
                            }

                            if (line == string.Empty)
                            {
                                break;
                            }

                            changeAdd.Attributes.Add(LDIFAttribute.Parse(line));
                        }

                        result.Add(changeAdd);
                    }
                }
            }

            return result;
        }
    }
}

using ClubAdministration.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace ClubAdministration.ImportConsole
{
  public class ImportController
  {
    const string FileName = "members.csv";

    public static async Task<MemberSection[]> ReadFromCsvAsync()
    {
            string[][] matrix = await MyFile.ReadStringMatrixFromCsvAsync(FileName, true);
            var member = matrix
                .Select(mem => new Member
                {
                    FirstName = mem[1],
                    LastName = mem[0],

                }).GroupBy(line => line.FirstName + line.LastName).Select(m => m.First()).ToArray();

            var section = matrix
                .GroupBy(line => line[2])
                .Select(sec => new Section
                {
                    Name = sec.Key
                }).ToArray();

            var memberSection = matrix
                .Select(ms => new MemberSection
                {
                    Member = member.Single(s => s.FirstName == ms[1] && s.LastName == ms[0]),
                    Section = section.Single(s => s.Name == ms[2])
                }).ToArray();

            return memberSection;
    }

  }
}

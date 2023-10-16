using System;
using System.Reflection;

namespace BW_Team_5_ClinicaVeterinaria.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
using System.Collections.Generic;
using System.Web.UI;

namespace OpenB.Web.Elements
{
    public class Component : BaseModelBoundElementContainer, IElementContainer
    {
        public Component(RenderContext renderContext, string key) : base(renderContext, key)
        {
          
        }

        protected override void InnerInitialize()
        {
             
        }

        public override void Render(HtmlTextWriter textWriter)
        {
            var controllerId = string.Join("_", this.Key, "Controller");

            textWriter.AddAttribute("ng-controller", controllerId);
            textWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            textWriter.RenderBeginTag(HtmlTextWriterTag.Form);

            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "container");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            RenderChildren(textWriter);
                       
            textWriter.RenderEndTag();
            textWriter.RenderEndTag();
            textWriter.WriteLine($"<pre>user = {{{{{this.Key} | json}}}}</pre>");

            textWriter.WriteLine($"<script>angular.module('formExample', []).controller('{controllerId}', ['$scope', function($scope) {{$scope.master = {{ }}; $scope.update = function({this.Key}) {{$scope.master = angular.copy({this.Key}); }}; $scope.reset = function() {{ $scope.{this.Key} = angular.copy($scope.master); }}; $scope.reset(); }}]);</script>");


            
        }
    }
}

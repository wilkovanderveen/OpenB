using System.Web.UI;
using OpenB.Core;
using OpenB.Modeling;
using OpenB.Web.Elements;

namespace OpenB.Web
{
    public abstract class BaseModelBoundElementContainer : BaseElementContainer 
    {
        protected object Model { get; private set; }

        [MarkupLanguageProperty("Model")]
        public string ModelBindingExpression { get; set; }

        [MarkupLanguageProperty("ModelDefinition")]
        public string ModelDefinitionBindingExpression { get; set; }

        [MarkupLanguageProperty("Repository")]
        public string RepositoryBindingExpression { get; set; }

        public void Bind()
        {
            IModelFactory modelFactory = new ModelFactory(Project.GetInstance(RepositoryBindingExpression));
            Model = modelFactory.ReadInstance(ModelDefinitionBindingExpression, ModelBindingExpression);
        }

        protected BaseModelBoundElementContainer(RenderContext renderContext, string key) : base(renderContext, key)
        {
        }
    }

    public abstract class BaseModelBoundElement : BaseElement
    {
        protected object Model { get; private set; }

        protected BaseModelBoundElement(RenderContext renderContext, string key) : base(renderContext, key)
        {

        }      

        [MarkupLanguageProperty("Model")]
        public string ModelBindingExpression { get; set; }

        [MarkupLanguageProperty("ModelDefinition")]
        public string ModelDefinitionBindingExpression { get; set; }

        [MarkupLanguageProperty("Repository")]
        public string RepositoryBindingExpression { get; set; }

        public void Bind()
        {
            IModelFactory modelFactory = new ModelFactory(Project.GetInstance(RepositoryBindingExpression));
            Model = modelFactory.ReadInstance(ModelDefinitionBindingExpression, ModelBindingExpression);
        }
    }
}
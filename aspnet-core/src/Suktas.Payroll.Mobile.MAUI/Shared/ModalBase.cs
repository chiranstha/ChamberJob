using Suktas.Payroll.Core.Dependency;
using Suktas.Payroll.Mobile.MAUI.Services.UI;

namespace Suktas.Payroll.Mobile.MAUI.Shared
{
    public abstract class ModalBase : PayrollComponentBase
    {
        protected ModalManagerService ModalManager { get; set; }

        public abstract string ModalId { get; }

        public ModalBase()
        {
            ModalManager = DependencyResolver.Resolve<ModalManagerService>();
        }

        public virtual async Task Show()
        {
            await ModalManager.Show(JS, ModalId);
            StateHasChanged();
        }

        public virtual async Task Hide()
        {
            await ModalManager.Hide(JS, ModalId);
        }
    }
}

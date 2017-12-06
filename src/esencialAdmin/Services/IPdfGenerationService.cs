using esencialAdmin.Models.PdfViewModels;
using esencialAdmin.Models.SubscriptionViewModels;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public interface IPdfGenerationService
    {
        PdfCertificateViewModel getCertificateModel(int customerID);

        List<PdfSingleAdressViewModel> getAdressLabelsModel(SubscriptionIndexViewModel filter);

        List<PdfSingleBottleLabelViewModel> getBottleLabels(SubscriptionIndexViewModel filter);

        List<PdfSinglePictureTemplateViewModel> getPictureTemplatesModel(SubscriptionIndexViewModel filter);

        
    }
}
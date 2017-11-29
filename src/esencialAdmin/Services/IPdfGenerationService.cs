using esencialAdmin.Models.PdfViewModels;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public interface IPdfGenerationService
    {
        PdfCertificateViewModel getCertificateModel(int customerID);

        List<PdfSingleAdressViewModel> getAdressLabelsModel();

    }
}
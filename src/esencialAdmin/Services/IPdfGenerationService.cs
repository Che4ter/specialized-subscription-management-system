﻿using esencialAdmin.Models.PdfViewModels;
using esencialAdmin.Models.SubscriptionViewModels;
using System.Collections.Generic;

namespace esencialAdmin.Services
{
    public interface IPdfGenerationService
    {
        PdfCertificateViewModel getCertificateModel(int customerID);

        List<PdfSingleAdressViewModel> getAdressLabelsModel(SubscriptionIndexViewModel filter);

    }
}
namespace HospiceToolsChallenge.Api.Security.Cors
{
    public record CorsConfiguration
    {
        /// <summary>The headers which need to be allowed in the CORS policy.</summary>
        public string[] Headers { get; set; }

        /// <summary>The methods which need to be added to the CORS policy.</summary>
        public string[] Methods { get; set; }

        /// <summary>The origins that are allowed in the CORS policy.</summary>
        public string[] Origins { get; set; }

        /// <summary>The headers that are exposed in the CORS policy.</summary>
        public string[] ExposedHeaders { get; set; }
    }
}

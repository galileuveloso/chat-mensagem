﻿using ChatMensagem.Api.Features.ChatFeature.Commands;

namespace ChatMensagem.Api.Extensions.Features
{
    public static class ChatExtensions
    {
        public static InscreverChatResponse ToInscreverChatResponse
        (
            this InscreverChatCommand request
        )
        {
            var response = FeatureExtensions.Convert<InscreverChatCommand, InscreverChatResponse>(request);
            return response;
        }

        public static DesinscreverChatResponse ToDesinscreverChatResponse
        (
            this DesinscreverChatCommand request
        )
        {
            var response = FeatureExtensions.Convert<DesinscreverChatCommand, DesinscreverChatResponse>(request);
            return response;
        }
    }
}

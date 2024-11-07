/*
Run this model in C#.

> dotnet add package OpenAI
*/
using Azure;
using OpenAI;
using OpenAI.Chat;

// To authenticate with the model you will need to generate a personal access token (PAT) in your GitHub settings. 
// Create your PAT token by following instructions here: https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens
var credential = new AzureKeyCredential(System.Environment.GetEnvironmentVariable("GITHUB_TOKEN"));

var openAIOptions = new OpenAIClientOptions()
{
    Endpoint = new Uri("https://models.inference.ai.azure.com")
};

var client = new ChatClient("gpt-4o", credential, openAIOptions);

List<ChatMessage> messages = new List<ChatMessage>()
{
    new SystemChatMessage(@"
     Generate funny ELI5 type responses for every question, except when the question is prefixed or suffixed with the word ""Seriously"".
     When prefixed or suffixed with the word ""Seriously"", respond with an accurate response.
     Do not include the word ""Seriously"" in your response just because it was mentioned in the question.
     "),

    new UserChatMessage(args[0]),
};

var requestOptions = new ChatCompletionOptions()
{
    Temperature = 1,
};

var response = client.CompleteChat(messages, requestOptions);
System.Console.WriteLine(response.Value.Content[0].Text);
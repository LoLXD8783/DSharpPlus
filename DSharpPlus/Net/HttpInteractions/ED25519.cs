using System;
using System.Runtime.InteropServices;

namespace DSharpPlus.Net.HttpInteractions;

internal static partial class Ed25519
{
    internal const int SignatureBytes = 64;
    internal const int PublicKeyBytes = 32;

    internal static unsafe bool TryVerifySignature(ReadOnlySpan<byte> body, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> signature)
    {
        if (signature.Length != SignatureBytes)
        {
            throw new ArgumentOutOfRangeException("signature.Length");
        }
        if (publicKey.Length != PublicKeyBytes)
        {
            throw new ArgumentOutOfRangeException("publicKey.Length");
        }

        fixed (byte* signaturePtr = signature)
        fixed (byte* messagePtr = body)
        fixed (byte* publicKeyPtr = publicKey)
        {
            return Bindings.crypto_sign_ed25519_verify_detached(signaturePtr, messagePtr, (ulong)body.Length, publicKeyPtr) == 0;
        }
    }

    // Ed25519.Bindings is a nested type to lazily load sodium. the native load is done by the static constructor,
    // which will not be executed unless this code actually gets used. since we cannot rely on sodium being present at all
    // times, it is imperative this remains a nested type.
    private static partial class Bindings
    {
        static Bindings()
        {
            if (sodium_init() == -1)
            {
                throw new InvalidOperationException("Failed to initialize libsodium.");
            }
        }

        // LibraryImport is not really required here
        [DllImport("sodium")]
        private static extern unsafe int sodium_init();

        [DllImport("sodium")]
        internal static extern unsafe int crypto_sign_ed25519_verify_detached(byte* signature, byte* message, ulong messageLength, byte* publicKey);
    }
}

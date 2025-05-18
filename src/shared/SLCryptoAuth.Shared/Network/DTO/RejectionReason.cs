namespace SLCryptoAuth.Network.DTO;

public enum RejectionReason : byte
{
    // Token: 0x0400067F RID: 1663
    NotSpecified,
    // Token: 0x04000680 RID: 1664
    ServerFull,
    // Token: 0x04000681 RID: 1665
    InvalidToken,
    // Token: 0x04000682 RID: 1666
    VersionMismatch,
    // Token: 0x04000683 RID: 1667
    Error,
    // Token: 0x04000684 RID: 1668
    AuthenticationRequired,
    // Token: 0x04000685 RID: 1669
    Banned,
    // Token: 0x04000686 RID: 1670
    NotWhitelisted,
    // Token: 0x04000687 RID: 1671
    GloballyBanned,
    // Token: 0x04000688 RID: 1672
    Geoblocked,
    // Token: 0x04000689 RID: 1673
    Custom,
    // Token: 0x0400068A RID: 1674
    ExpiredAuth,
    // Token: 0x0400068B RID: 1675
    RateLimit,
    // Token: 0x0400068C RID: 1676
    Challenge,
    // Token: 0x0400068D RID: 1677
    InvalidChallengeKey,
    // Token: 0x0400068E RID: 1678
    InvalidChallenge,
    // Token: 0x0400068F RID: 1679
    Redirect,
    // Token: 0x04000690 RID: 1680
    Delay,
    // Token: 0x04000691 RID: 1681
    VerificationAccepted,
    // Token: 0x04000692 RID: 1682
    VerificationRejected,
    // Token: 0x04000693 RID: 1683
    CentralServerAuthRejected
}

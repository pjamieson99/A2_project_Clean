Public Class CLeg
    Public Speed As Integer
    Public Clock As Integer
    Public Angle As Integer
    Public GoThroughClock As Integer

    Public LXpos As Double
    Public LYpos As Double

    Public LPx1 As Double
    Public LPx2 As Double
    Public LPy1 As Double
    Public LPy2 As Double

    Public LP1 As PointF
    Public LP2 As PointF

    Public Sub New(x As Integer, y As Integer, rnd As Random)
        'set properties
        LXpos = x
        LYpos = y
        Lpx1 = LXpos
        Lpy1 = LYpos
        Lpx2 = LXpos
        Lpy2 = LYpos + 100
        Speed = rnd.Next(1, 10)
        clock = rnd.Next(5, 80)
        Angle = rnd.Next(20, 60)
        GoThroughClock = 0
        Lp1 = New Point(LXpos, LYpos)
        Lp2 = New Point(LXpos, LYpos + 100)
    End Sub
End Class

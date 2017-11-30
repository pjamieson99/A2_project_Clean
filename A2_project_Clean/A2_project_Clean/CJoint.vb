Public Class CJoint

    Private width As Integer
    Private diameter As Integer
    Public JYpos As Integer
    Public JXpos As Integer

    'set properties
    Public Sub New(x As Integer, y As Integer, w As Integer, d As Integer)

        width = w
        diameter = d
        JYpos = y
        JXpos = x

    End Sub

    'draw
    Public Sub Draw(g As Graphics)
        g.FillEllipse(Brushes.Black, JXpos - 5, JYpos - 5, width, diameter)
    End Sub

    'rise the joint towards the line point 1
    Sub StickToLeg(p1 As PointF)
        JYpos = p1.Y
        JXpos = p1.X
    End Sub

End Class

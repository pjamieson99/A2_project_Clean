Public Class Form1
    Dim Floor As CFloor
    Dim NumOfAnimals As Integer = 2
    Dim Base(NumOfAnimals - 1) As CBody
    Dim checking As Boolean
    Public NumOfLegs As Integer = 3
    Public NumOfLayers As Integer = 2
    Dim Line(NumOfLegs - 1, NumOfLayers - 1) As CLeg
    Dim Joint(NumOfLegs - 1, NumOfLayers - 1) As CJoint
    Dim BodyLines(NumOfAnimals - 1)
    Dim BodyJoints(NumOfAnimals - 1)
    Dim Rnd As New Random
    Dim TotalDistance As Double
    Dim Animal(NumOfAnimals - 1) As CCreature




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'create floor object
        Floor = New CFloor(400)

        For n = 0 To NumOfAnimals - 1
            Base(n) = New CBody(100, 150, 300, 150)
        Next




        For n = 0 To NumOfAnimals - 1
            Rnd.Next()

            For y = 0 To NumOfLayers - 1
                For x = 0 To NumOfLegs - 1

                    Line(x, y) = New CLeg(100 + 100 * x, 50 + 100 * y, Rnd)
                    Joint(x, y) = New CJoint(100 + 100 * x, 50 + 100 * y, 10, 10)

                Next
            Next
            BodyLines(n) = Line
            BodyJoints(n) = Joint
        Next


        For n = 0 To NumOfAnimals - 1

            Animal(n) = New CCreature(Base(n), BodyLines(n), BodyJoints(n), Floor)
        Next



    End Sub




    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'keep display refreshing, loop through display
        Display.Refresh()
    End Sub




    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint


        Dim g As Graphics
        g = e.Graphics

        For x = 0 To NumOfAnimals - 1
            If Animal(x).TimeCounter < 500 And Animal(x).DeadCounter < 125 Then

                Animal(x).NextMove(g)
            End If

        Next


    End Sub

    Function CheckFloor(floor As CFloor, point As Double)

        If floor.ypos - 1 <= point Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub FindFitness(animal() As CCreature)
        TotalDistance = 0
        For n = 0 To NumOfAnimals - 1
            TotalDistance += animal(n).Body.CoM.X
        Next
        For n = 0 To NumOfAnimals - 1
            animal(n).Fitness = animal(n).Body.CoM.X / TotalDistance * 100
        Next

    End Sub



End Class

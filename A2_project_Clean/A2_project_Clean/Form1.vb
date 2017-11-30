Public Class Form1
    Dim Floor As CFloor
    Dim NumOfAnimals As Integer = 1
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
    Dim Animal As New List(Of CCreature)
    Dim ChosenParents(1) As Integer

    Dim Generation As Integer = 1
    Dim Drawing As Boolean = True
    Dim MutationRate As Double = 0.05

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'create floor object
        Floor = New CFloor(400)

        For n = 0 To NumOfAnimals - 1
            Base(n) = New CBody(100, 150, 300, 150)
        Next

        For n = 0 To NumOfAnimals - 1
            For y = 0 To NumOfLayers - 1
                For x = 0 To NumOfLegs - 1
                    Line(x, y) = New CLeg(100 + 100 * x, 50 + 100 * y, 1, 1, 1, 1, Rnd)
                    Joint(x, y) = New CJoint(100 + 100 * x, 50 + 100 * y, 10, 10)

                Next
            Next
            Dim TempLine(NumOfLegs - 1, NumOfLayers - 1) As CLeg
            TempLine = Line.Clone()
            BodyLines(n) = TempLine
            BodyJoints(n) = Joint
            Animal.Add(New CCreature(Base(n), BodyLines(n), BodyJoints(n), Floor))
        Next



            'For n = 0 To NumOfAnimals - 1

        '    Animal(n) = New CCreature(Base(n), BodyLines(n), BodyJoints(n), Floor)
        'Next
    End Sub




    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'keep display refreshing, loop through display
        Display.Refresh()
    End Sub




    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint

        Dim g As Graphics
        g = e.Graphics
        Floor.Draw(g)
            g.DrawString("Generation: " & Generation, DefaultFont, Brushes.Black, New Point(0, 0))

            Dim AnimalAlive As Boolean = False

            For x = 0 To NumOfAnimals - 1
                If Animal(x).TimeCounter < 500 And Animal(x).DeadCounter < 125 Then
                Animal(x).NextMove(g)
                AnimalAlive = True
                Else
                    Animal(x).IsDead = True
                End If

            Next

        If Not AnimalAlive Then
            GeneticAlgorithm()
            'genetic algorithm
            Generation += 1

        End If
        If Not Drawing Then
            Display.Refresh()
        End If


    End Sub




    Function CheckFloor(floor As CFloor, point As Double)

        If floor.ypos - 1 <= point Then
            Return True
        Else
            Return False
        End If
    End Function



    Sub GeneticAlgorithm()
        FindFitness()
        KillWeakest()
        CrossOver()
    End Sub

    Sub KillWeakest()
        Dim DeathPool As New List(Of CCreature)

        For X = 0 To NumOfAnimals - 1
            For Y = 0 To Math.Min(100, (Animal(X).Fitness ^ (-1) * 100))
                DeathPool.Add(Animal(X))
            Next
        Next

        Dim ToDie As New List(Of CCreature)

        For x = 0 To NumOfAnimals / 2 - 1
            Dim SelectedCreature As CCreature = DeathPool(Math.Floor(Rnd.Next(DeathPool.Count)))
            While ToDie.Contains(SelectedCreature)
                SelectedCreature = DeathPool(Math.Floor(Rnd.Next(DeathPool.Count)))
            End While
            ToDie.Add(SelectedCreature)
        Next

        For Each DeadAnimal In ToDie
            Animal.Remove(DeadAnimal)
        Next


    End Sub


    Sub FindFitness()
        TotalDistance = 0
        For n = 0 To NumOfAnimals - 1
            If Animal(n).Body.CoM.X > 0 Then
                TotalDistance += Animal(n).Body.CoM.X
            End If
        Next
        For n = 0 To NumOfAnimals - 1
            If Animal(n).Body.CoM.X > 0 Then
                Animal(n).Fitness = Animal(n).Body.CoM.X / TotalDistance * 100
            Else
                Animal(n).Fitness = 0
            End If


        Next

    End Sub


    Sub CrossOver()
        Dim MatingPool As New List(Of CCreature)

        For X = 0 To Animal.Count - 1
            For Y = 0 To Animal(X).Fitness
                MatingPool.Add(Animal(X))
            Next
        Next

        For I = 0 To NumOfAnimals / 2 - 1
            Dim MotherIndex As Integer = 0
            Dim FatherIndex As Integer = 0
            Dim Parents As New List(Of CCreature)
            While MotherIndex = FatherIndex
                MotherIndex = Math.Floor(Rnd.Next(MatingPool.Count))
                FatherIndex = Math.Floor(Rnd.Next(MatingPool.Count))
            End While
            Parents.Add(MatingPool(MotherIndex))
            Parents.Add(MatingPool(FatherIndex))
            For y = 0 To NumOfLayers - 1
                For x = 0 To NumOfLegs - 1
                    Dim NewSpeed As Integer = (Parents(0).line(x, y).Speed + Parents(1).line(x, y).Speed) / 2
                    If Rnd.Next(1) < MutationRate Then
                        NewSpeed = Rnd.Next(1, 10)
                    End If
                    Dim NewAngle As Integer = (Parents(0).line(x, y).Angle + Parents(1).line(x, y).Angle) / 2
                    If Rnd.Next(1) < MutationRate Then
                        NewSpeed = Rnd.Next(50, 180)
                    End If
                    Dim NewClock As Integer = (Parents(0).line(x, y).Clock + Parents(1).line(x, y).Clock) / 2
                    If Rnd.Next(1) < MutationRate Then
                        NewSpeed = Rnd.Next(5, 50)
                    End If
                    Dim NewGTClock As Integer = (Parents(0).line(x, y).GoThroughClock + Parents(1).line(x, y).GoThroughClock) / 2
                    If Rnd.Next(1) < MutationRate Then
                        NewSpeed = Rnd.Next(5, 50)
                    End If
                    'Line(x, y) = New CLeg(100 + 100 * x, 50 + 100 * y, Parents(Math.Floor(Rnd.Next(Parents.Count))).line(x, y).Speed, Parents(Math.Floor(Rnd.Next(Parents.Count))).line(x, y).Clock, Parents(Math.Floor(Rnd.Next(Parents.Count))).line(x, y).Angle, Parents(Math.Floor(Rnd.Next(Parents.Count))).line(x, y).GoThroughClock)
                    Line(x, y) = New CLeg(100 + 100 * x, 50 + 100 * y, NewSpeed, NewClock, NewAngle, NewGTClock)
                    Joint(x, y) = New CJoint(100 + 100 * x, 50 + 100 * y, 10, 10)
                Next
            Next
            Animal.Add(New CCreature(New CBody(100, 150, 300, 150), Line.Clone(), Joint.Clone(), Floor))
        Next

    End Sub

    Public Sub KeyPressed(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.B Then
            Drawing = Not Drawing
        End If

    End Sub

End Class

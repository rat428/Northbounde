using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public class AllNotesViewModel : BaseViewModel
    {
        public AllNotesViewModel()
        {

        }
    }
}
/*
 <VerticalStackLayout x:Name="MainPanel" Margin="16">
            <VerticalStackLayout>
                <!-- Syncfusion DataGrid -->
                <syncfusion:SfDataGrid x:Name="RecordsDataGrid"
                                ItemsSource="{Binding Records}"
                                AutoGenerateColumnsMode="None"
                                AllowEditing="True"
                                GridLinesVisibility="Both"
                                SelectionMode="Single">
                    <!-- Service Location Column -->
                    <syncfusion:SfDataGrid.Columns>
                        <syncfusion:DataGridTemplateColumn MappingName="ServiceLocation" HeaderText="Service Location">
                            <syncfusion:DataGridTemplateColumn.CellTemplate>
                                <DataTemplate>
                                    <Picker ItemsSource="{Binding DataContext.ServiceLocations, Source={x:Reference RecordsDataGrid}}"
                                    SelectedItem="{Binding ServiceLocation}" />
                                </DataTemplate>
                            </syncfusion:DataGridTemplateColumn.CellTemplate>
                        </syncfusion:DataGridTemplateColumn>

                        <!-- Other Location Column -->
                        <syncfusion:DataGridTemplateColumn MappingName="OtherLocation" HeaderText="Other Location">
                            <syncfusion:DataGridTemplateColumn.CellTemplate>
                                <DataTemplate>
                                    <Entry Text="{Binding OtherLocation}" Placeholder="Enter custom location" />
                                </DataTemplate>
                            </syncfusion:DataGridTemplateColumn.CellTemplate>
                        </syncfusion:DataGridTemplateColumn>

                        <!-- Session Details Column -->
                        <syncfusion:DataGridTemplateColumn MappingName="SessionDetails" HeaderText="Session Details">
                            <syncfusion:DataGridTemplateColumn.CellTemplate>
                                <DataTemplate>
                                    <Entry Text="{Binding SessionDetails}" Placeholder="Enter session details" />
                                </DataTemplate>
                            </syncfusion:DataGridTemplateColumn.CellTemplate>
                        </syncfusion:DataGridTemplateColumn>

                        <!-- Attendance Signature Column -->
                        <syncfusion:DataGridTemplateColumn MappingName="AttendanceSignature" HeaderText="Signature">
                            <syncfusion:DataGridTemplateColumn.CellTemplate>
                                <DataTemplate>
                                    <Entry Text="{Binding AttendanceSignature}" Placeholder="Enter signature" />
                                </DataTemplate>
                            </syncfusion:DataGridTemplateColumn.CellTemplate>
                        </syncfusion:DataGridTemplateColumn>
                    </syncfusion:SfDataGrid.Columns>
                </syncfusion:SfDataGrid>

                <!-- Action Buttons -->
                <HorizontalStackLayout Margin="0,16">
                    <Button Text="Save Changes" Command="{Binding SaveChangesCommand}" Margin="4" />
                    <Button Text="Refresh" Command="{Binding RefreshCommand}" Margin="4" />
                </HorizontalStackLayout>
            </VerticalStackLayout>
            <!--First Section-->
            <Border>
                <toolkit:Expander IsExpanded="{Binding IsExpanded1Visible,Mode=TwoWay}">
                    <toolkit:Expander.Header>
                        <Label Text=" To replace with section Name" FontSize="20" />
                    </toolkit:Expander.Header>
                    <toolkit:Expander.Content>
                        <sections:Section1 BindingContext="{Binding .,Mode=TwoWay}"  />
                    </toolkit:Expander.Content>
                </toolkit:Expander>
            </Border>

            <!--Second Section-->
            <Border>
                <toolkit:Expander IsExpanded="{Binding IsExpanded2Visible,Mode=TwoWay}">
                    <toolkit:Expander.Header>
                        <Label Text=" To replace with section Name" FontSize="20" />
                    </toolkit:Expander.Header>
                    <toolkit:Expander.Content>
                        <sections:Section2 BindingContext="{Binding .,Mode=TwoWay}"  />
                    </toolkit:Expander.Content>
                </toolkit:Expander>
            </Border>

            <!--Third Section-->
            <Border>
                <toolkit:Expander IsExpanded="{Binding IsExpanded3Visible,Mode=TwoWay}">
                    <toolkit:Expander.Header>
                        <Label Text=" To replace with section Name" FontSize="20" />
                    </toolkit:Expander.Header>
                    <toolkit:Expander.Content>
                        <sections:Section3 BindingContext="{Binding .,Mode=TwoWay}"  />
                    </toolkit:Expander.Content>
                </toolkit:Expander>
            </Border>
        </VerticalStackLayout>
 */

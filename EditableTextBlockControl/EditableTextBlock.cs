using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditableTextBlockControl
{
    public class EditableTextBlock : Control
    {
        private TextBlock? _textBlock;
        private TextBox? _textBox;
        private Button? _editButton;
        private Button? _saveButton;
        private Button? _cancelButton;
        private string _originalText = string.Empty;
        
        private const int DEFAULT_MAX_LENGTH = 100;
        private const string PART_TEXT_BLOCK = "PART_TextBlock";
        private const string PART_TEXT_BOX = "PART_TextBox";
        private const string PART_EDIT_BUTTON = "PART_EditButton";
        private const string PART_SAVE_BUTTON = "PART_SaveButton";
        private const string PART_CANCEL_BUTTON = "PART_CancelButton";

        static EditableTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBlock), 
                new FrameworkPropertyMetadata(typeof(EditableTextBlock)));
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(EditableTextBlock),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsEditingProperty =
            DependencyProperty.Register(nameof(IsEditing), typeof(bool), typeof(EditableTextBlock),
                new PropertyMetadata(false));

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register(nameof(MaxLength), typeof(int), typeof(EditableTextBlock),
                new PropertyMetadata(DEFAULT_MAX_LENGTH));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsEditing
        {
            get => (bool)GetValue(IsEditingProperty);
            set => SetValue(IsEditingProperty, value);
        }

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public override void OnApplyTemplate()
        {
            UnsubscribeEvents();

            base.OnApplyTemplate();

            _textBlock = GetTemplateChild(PART_TEXT_BLOCK) as TextBlock;
            _textBox = GetTemplateChild(PART_TEXT_BOX) as TextBox;
            _editButton = GetTemplateChild(PART_EDIT_BUTTON) as Button;
            _saveButton = GetTemplateChild(PART_SAVE_BUTTON) as Button;
            _cancelButton = GetTemplateChild(PART_CANCEL_BUTTON) as Button;

            SubscribeEvents();
            _originalText = Text;
        }

        private void SubscribeEvents()
        {
            if (_textBox != null)
            {
                _textBox.LostFocus += HandleTextBoxLostFocus;
                _textBox.KeyDown += HandleTextBoxKeyDown;
                _textBox.TextChanged += HandleTextBoxTextChanged;
            }

            if (_editButton != null)
            {
                _editButton.Click += HandleEditButtonClick;
            }

            if (_saveButton != null)
            {
                _saveButton.Click += HandleSaveButtonClick;
            }

            if (_cancelButton != null)
            {
                _cancelButton.Click += HandleCancelButtonClick;
            }
        }

        private void UnsubscribeEvents()
        {
            if (_textBox != null)
            {
                _textBox.LostFocus -= HandleTextBoxLostFocus;
                _textBox.KeyDown -= HandleTextBoxKeyDown;
                _textBox.TextChanged -= HandleTextBoxTextChanged;
            }

            if (_editButton != null)
            {
                _editButton.Click -= HandleEditButtonClick;
            }

            if (_saveButton != null)
            {
                _saveButton.Click -= HandleSaveButtonClick;
            }

            if (_cancelButton != null)
            {
                _cancelButton.Click -= HandleCancelButtonClick;
            }
        }

        private void HandleTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_textBox != null && _textBox.Text.Length > MaxLength)
            {
                _textBox.Text = _textBox.Text[..MaxLength];
                _textBox.CaretIndex = MaxLength;
            }
        }

        private void HandleTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsMouseOver)
            {
                HandleSaveButtonClick(sender, e);
            }
        }

        private void HandleTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    HandleSaveButtonClick(sender, e);
                    break;
                case Key.Escape:
                    HandleCancelButtonClick(sender, e);
                    break;
            }
        }

        private void HandleEditButtonClick(object sender, RoutedEventArgs e)
        {
            _originalText = Text;
            IsEditing = true;
            _textBox?.Focus();
        }

        private void HandleSaveButtonClick(object sender, RoutedEventArgs e)
        {
            _originalText = Text;
            IsEditing = false;
            _textBlock?.Focus();
        }

        private void HandleCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Text = _originalText;
            IsEditing = false;
            _textBlock?.Focus();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == MaxLengthProperty && _textBox != null)
            {
                _textBox.MaxLength = MaxLength;
            }
        }
    }
}

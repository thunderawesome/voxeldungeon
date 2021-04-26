using System;
using System.Threading;
using UnityEngine;

namespace Runtime.Common
{

    /// <summary>
    ///     ZAS: This is a disposable token source that will cancel if the app stops playing
    /// </summary>
    public class UnityTokenSource : IDisposable
    {

        #region Public Properties

        public CancellationToken Token => m_tokenSource.Token;

        #endregion

        #region Public Constructors

        public UnityTokenSource(CancellationToken token)
            : this(CancellationTokenSource.CreateLinkedTokenSource(token)) { }

        public UnityTokenSource()
            : this(new CancellationTokenSource()) { }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            DisposeInternal();
        }

        public void Cancel()
        {
            m_tokenSource.Cancel();
        }

        public void CancelAfter(TimeSpan fromSeconds)
        {
            m_tokenSource.CancelAfter(fromSeconds);
        }

        #endregion

        #region Private Constructors

        private UnityTokenSource(CancellationTokenSource tokenSource)
        {
            _ = tokenSource ?? throw new ArgumentNullException(nameof(tokenSource));

            m_tokenSource = tokenSource;
            Application.quitting += m_tokenSource.Cancel;
        }

        #endregion

        #region Private Methods

        private void DisposeInternal()
        {
            if (m_isDisposed)
            {
                return;
            }

            Application.quitting -= m_tokenSource.Cancel;
            m_isDisposed = true;
        }

        #endregion

        #region Private Fields

        private readonly CancellationTokenSource m_tokenSource = null;
        private bool m_isDisposed = false;

        #endregion

        ~UnityTokenSource()
        {
            DisposeInternal();
        }

    }

}
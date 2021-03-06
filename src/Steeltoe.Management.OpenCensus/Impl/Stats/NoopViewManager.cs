﻿using Steeltoe.Management.Census.Common;
using Steeltoe.Management.Census.Tags;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Steeltoe.Management.Census.Stats
{
    [Obsolete("Use OpenCensus project packages")]
    internal sealed class NoopViewManager : ViewManagerBase
    {
        private static readonly ITimestamp ZERO_TIMESTAMP = Timestamp.Create(0, 0);

        private readonly IDictionary<IViewName, IView> registeredViews = new Dictionary<IViewName, IView>();

        // Cached set of exported views. It must be set to null whenever a view is registered or
        // unregistered.
        private volatile ISet<IView> exportedViews;

        public override void RegisterView(IView newView)
        {
            if (newView == null)
            {
                throw new ArgumentNullException(nameof(newView));
            }
            lock (registeredViews)
            {
                exportedViews = null;
                registeredViews.TryGetValue(newView.Name, out IView existing);
                if (!(existing == null || newView.Equals(existing)))
                {
                    throw new ArgumentException("A different view with the same name already exists.");
                }

                if (existing == null)
                {
                    registeredViews.Add(newView.Name, newView);
                }
            }
        }


        public override IViewData GetView(IViewName name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));

            }
            lock (registeredViews)
            {
                registeredViews.TryGetValue(name, out IView view);
                if (view == null)
                {
                    return null;
                }
                else
                {
                    return ViewData.Create(
                        view,
                        new Dictionary<TagValues, IAggregationData>(),
                        ZERO_TIMESTAMP,
                        ZERO_TIMESTAMP);
                }
            }
        }


        public override ISet<IView> AllExportedViews
        {
            get
            {
                ISet<IView> views = exportedViews;
                if (views == null)
                {
                    lock (registeredViews)
                    {
                        exportedViews = views = FilterExportedViews(registeredViews.Values);
                        return ImmutableHashSet.CreateRange(exportedViews);
                    }
                }
                return views;
            }
        }

        // Returns the subset of the given views that should be exported

        private static ISet<IView> FilterExportedViews(ICollection<IView> allViews)
        {
            ISet<IView> views = new HashSet<IView>();
            foreach (IView view in allViews)
            {
                //if (view.getWindow() instanceof View.AggregationWindow.Interval) {
                //    continue;
                //}
                views.Add(view);
            }
            return views;
        }
    }
}
